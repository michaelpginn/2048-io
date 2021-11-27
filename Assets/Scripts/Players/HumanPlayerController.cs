using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Starter code inspired by https://www.youtube.com/watch?v=SeBEvM2zMpY


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class HumanPlayerController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerInput playerInput;
    public static HumanPlayerController instance; 
    private Transform cameraTransform;

    private InputAction moveAction; 
    private InputAction jumpAction;
    private InputAction shootAction;

    static public HumanPlayerController humanPlayerInstance;

    /// <summary>
    /// Stores essential information about the player such as their level and type
    /// </summary>
    private PlayerModel playerModel;

    private void Awake()
    {
        playerModel = GetComponent<PlayerModel>();
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        Cursor.lockState = CursorLockMode.Locked;
        humanPlayerInstance = this;
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
        playerController.Shoot(cameraTransform.position, cameraTransform.forward);
    }

    void Update()
    {
        // Update playerController input values based on input
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        // Rotate player to face same as camera
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        playerController.SetMovement(move, jumpAction.triggered, targetRotation);

       
    }

    void OnDestroy() {
        // Load Death Scene
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
    }

   
}