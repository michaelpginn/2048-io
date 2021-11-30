using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;

[RequireComponent(typeof(PlayerModel), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Physics Constants")]
    [SerializeField]
    private float playerSpeed = 1000f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 10f;

    [Header("Game Objects")]
    public GameObject bulletPrefab;
    public Transform barrelTransform;
    public Transform bulletParent;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera aimCamera;
    AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip explodeSound;
    public AudioClip hitSound;

    // Fields for player controller sub-scripts to effect movement
    private Vector3 movementInput;
    private bool shouldJump;
    private Quaternion targetRotation;

    private Vector3 playerVelocity;
    private bool isDead = false;

    // Controls movement
    private CharacterController characterController;

    // A reference to the sides of the cube
    private TextMeshPro[] sides;

    //public static GameObject cpu;

    /// <summary> Stores essential information about the player such as their level and type </summary>
    private PlayerModel playerModel;


    private void Start()
    {
        playerModel = GetComponent<PlayerModel>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        sides = GetComponentsInChildren<TextMeshPro>();

        // Add scripts based on whether the player is a user or CPU
        if (playerModel.playerType == PlayerType.human)
        {
            gameObject.AddComponent<HumanPlayerController>();
        }
        else
        {
            gameObject.AddComponent<CPUPlayerController>();
        }

        UpdateSideNumberDisplay();
    }

    private void Update()
    {
        if (isDead)
        {
            // We are in the process of exploding, no moving.
            return;
        }

        // These values are dependent on the input, set by SetMovement.
        bool groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        
        characterController.Move(movementInput * Time.deltaTime * playerSpeed);
        // Changes the height position of the player..
        if (shouldJump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Rotate player towards target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Explode()
    {
        isDead = true;
        audioSource.PlayOneShot(explodeSound);
        GetComponent<CharacterController>().detectCollisions = false;
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        explosion.Play();
        if (playerModel.playerType == PlayerType.CPU) {
            EnemyControl.instance.enemyCount--;
        }
        Destroy(gameObject, explosion.main.duration + 2);
    }

    /// <summary>Causes the player to shoot a bullet in the given direction.</summary>
    public void Shoot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.originPlayerController = this;
        audioSource.PlayOneShot(shootSound);
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            // We have a hit
            bulletController.target = hit.point;
            bulletController.hit = true;

            // Determine if it hit a player
            PlayerController otherPlayer = hit.collider.GetComponent<PlayerController>();
            if (otherPlayer != null && !otherPlayer.isDead)
            {
                // Show damage number
                if (playerModel.playerType == PlayerType.human)
                {
                    GameController.instance.ShowDamageNumber(otherPlayer.transform.position, playerModel.GetDamageAmount());
                }

                var otherPlayerAlive = otherPlayer.DecrementHealth(playerModel.GetDamageAmount());
                if (!otherPlayerAlive && otherPlayer.GetLevel() >= playerModel.level)
                {
                    // We have killed another player. If they were your level or higher, we should level up.
                    playerModel.LevelUp();

                    if (playerModel.playerType == PlayerType.human)
                    {
                        DamageNumber.damage = playerModel.GetDamageAmount();
                    }

                    UpdateSideNumberDisplay();
                    UpdateCameraForLevel();
                }
            }
        }
        else
        {
            bulletController.target = origin + direction * 25f;
            bulletController.hit = false;
        }
    }

    /// <summary>
    /// Moves the cameras back so they look good for the block size
    /// </summary>
    private void UpdateCameraForLevel()
    {
        // Update the camera to be in the right place.
        CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance += 1f;
            (componentBase as CinemachineFramingTransposer).m_ScreenY += 0.04f;
        }

        CinemachineComponentBase aimCamComponentBase = aimCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (aimCamComponentBase is CinemachineFramingTransposer)
        {
            var transposer = aimCamComponentBase as CinemachineFramingTransposer;
            transposer.m_CameraDistance += 0.35f;
            transposer.m_ScreenY += 0.02f;
            transposer.m_ScreenX -= 0.01f;
        }
    }

    /// <summary>
    /// Used to set the movement for a player.
    /// </summary>
    /// <param name="movementInput">A vector of which direction the player should move.</param>
    /// <param name="shouldJump">True if the player should jump.</param>
    /// <param name="targetRotation">The rotation the player should be facing.</param>
    public void SetMovement(Vector3 movementInput, bool shouldJump, Quaternion targetRotation)
    {
        this.movementInput = movementInput;
        this.shouldJump = shouldJump;
        this.targetRotation = targetRotation;
    }

    /// <summary>Decrements the health of a player by a given amount of damage.</summary>
    /// <returns>True if the player is still alive (health is greater than 0), and false if they have no more health.</returns>
    public bool DecrementHealth(int value)
    {
        var isAlive = playerModel.DecrementHealth(value);
        if (playerModel.playerType==PlayerType.human) {
            GameController.instance.GotShot();
            audioSource.PlayOneShot(hitSound);
        }
        
        if (!isAlive)
        {
            Explode();
            if (playerModel.playerType != PlayerType.human) {
                GameController.instance.IncrementScore(this.GetLevel().GetNumericalValue());
            }
        }
        return isAlive;
    }

    /// <summary>Updates the sides of the cube to display the correct number.</summary>
    private void UpdateSideNumberDisplay()
    {
        foreach(var side in sides)
        {
            side.text = playerModel.level.GetNumericalValue().ToString();
        }
    }

    public PlayerLevel GetLevel()
    {
        return playerModel.level;
    }

    public bool GetIsDead() {
        return isDead;
    }
}