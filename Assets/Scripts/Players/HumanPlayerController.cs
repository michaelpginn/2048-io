using UnityEngine;
using UnityEngine.InputSystem;

// Starter code inspired by https://www.youtube.com/watch?v=SeBEvM2zMpY


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class HumanPlayerController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerInput playerInput;

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
               int enemyHealth = enemy.GetHealth();
               if (enemyHealth <= 0) {
                   playerController.LevelUp(enemyLevel);
               }
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
}