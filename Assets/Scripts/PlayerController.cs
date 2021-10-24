using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(PlayerModel), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 1000f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    public float rotationSpeed = 10f;
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    public Transform barrelTransform;
    [SerializeField]
    public Transform bulletParent;
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;


    public static GameObject cpu;

    /// <summary>
    /// Stores essential information about the player such as their level and type
    /// </summary>
    private PlayerModel playerModel;


    private void Start()
    {
        playerModel = GetComponent<PlayerModel>();

        // Add scripts based on whether the player is a user or CPU
        if (playerModel.playerType == PlayerType.human)
        {
            var humanPlayerController = gameObject.AddComponent<HumanPlayerController>();
            humanPlayerController.playerSpeed = playerSpeed;
            humanPlayerController.jumpHeight = jumpHeight;
            humanPlayerController.gravityValue = gravityValue;
            humanPlayerController.rotationSpeed = 0.8f;
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

    // public void SetLevel(PlayerLevel level)
    // {
    //     playerModel.SetLevel(level);
    // }

    public void LevelUp(PlayerLevel level)
    {
        if (playerModel.level == level)
        {
            playerModel.LevelUp();
        }
    }
}