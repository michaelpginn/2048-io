using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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

    // Fields for player controller sub-scripts to effect movement
    [HideInInspector]
    /// <summary>An input vector for which direction the player should be moving.</summary>
    public Vector3 movementInput;
    [HideInInspector]
    public bool shouldJump;
    [HideInInspector]
    public Quaternion targetRotation;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    // Controls movement
    private CharacterController characterController;

    public static GameObject cpu;

    /// <summary> Stores essential information about the player such as their level and type </summary>
    private PlayerModel playerModel;


    private void Start()
    {
        playerModel = GetComponent<PlayerModel>();
        characterController = GetComponent<CharacterController>();

        // Add scripts based on whether the player is a user or CPU
        if (playerModel.playerType == PlayerType.human)
        {
            gameObject.AddComponent<HumanPlayerController>();
        }
        else
        {
            gameObject.AddComponent<CPUPlayerController>();
        }
    }

    public PlayerLevel GetLevel()
    {
        return playerModel.level;
    }

    public void LevelUp(PlayerLevel level)
    {
        if (playerModel.level == level)
        {
            playerModel.LevelUp();
        }
    }

    // Update function
    // These values are dependent on the input, set by

    private void Update()
    {
        groundedPlayer = characterController.isGrounded;
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
}