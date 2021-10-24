using UnityEngine;
using UnityEngine.InputSystem;

// Starter code inspired by https://www.youtube.com/watch?v=SeBEvM2zMpY


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class HumanPlayerController : MonoBehaviour
{
    private PlayerController playerController;
    private CharacterController controller;
    private PlayerInput playerInput;

    // These should be filled in by the Playercontorller
    public float playerSpeed;
    public float jumpHeight;
    public float gravityValue;
    public float rotationSpeed;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;


    private InputAction moveAction; 
    private InputAction jumpAction;
    private InputAction shootAction;

    /// <summary>
    /// Stores essential information about the player such as their level and type
    /// </summary>
    private PlayerModel playerModel;

    private void Awake()
    {
        playerModel = GetComponent<PlayerModel>();
        playerController = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];


        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(playerController.bulletPrefab, playerController.barrelTransform.position, Quaternion.identity, playerController.bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.parentLevel = playerModel.level;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;

            CPUPlayerController enemy = hit.collider.gameObject.GetComponent<CPUPlayerController>();
            if (enemy != null)
            {
                PlayerLevel enemyLevel = enemy.GetLevel();
                playerController.LevelUp(enemyLevel);
            }
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * 25f;
            bulletController.hit = false;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate player to face same as camera
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}