using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class CPUPlayerController : MonoBehaviour
{
    private PlayerController playerController;

    private float nextChangeTime = 0.0f;
    private float changePeriod = 1.0f;

    private Vector3 movementDirection;
    private bool shouldJump;
    private Quaternion targetRotation;
    
    //Outlets
    public TextMeshPro[] sides;
    
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        sides = GetComponentsInChildren<TextMeshPro>();

        movementDirection = new Vector3(1, 0, 0);
        shouldJump = false;
        targetRotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerController.SetMovement(movementDirection, shouldJump, targetRotation);

        if (Time.time > nextChangeTime)
        {
            nextChangeTime += changePeriod;

            // Generate new random values
            movementDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            shouldJump = false;
            targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
        
        foreach (TextMeshPro side in sides)
        {
            var sideText = (int) Mathf.Pow(2f, (int) playerController.GetLevel() + 1);
            side.text = sideText.ToString();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
       if (other.gameObject.GetComponent<BulletController>())
       {
           playerController.DecrementHealth();
           print("CPU health " + playerController.GetHealth());
           if (playerController.GetHealth() <= 0) {
               Explode();
           }
        // if (other.gameObject.GetComponent<BulletController>().parentLevel == playerController.GetLevel()) {
        //     Explode();
        // }
       }
    }

    public PlayerLevel GetLevel() {
        return playerController.GetLevel();
    }

    public int GetHealth() {
        return playerController.GetHealth();
    }

    void Explode()
    {
       ParticleSystem explosion = GetComponent<ParticleSystem>();
       explosion.Play();
       Destroy(gameObject, explosion.main.duration);
       // GameController.enemies -= 1;
       // GameController.spawnEnemy = true;
    }
}
