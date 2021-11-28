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
    private float accuracy = 0.5f;
    private bool shouldShoot = true;
    
    
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        movementDirection = new Vector3(1, 0, 0);
        shouldJump = false;
        targetRotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetIsDead())
        {
            // We are in the process of exploding, no moving.
            return;
        }

        playerController.SetMovement(movementDirection, shouldJump, targetRotation);

        if (Time.time > nextChangeTime)
        {
            nextChangeTime += changePeriod;
            if (Vector3.Distance(HumanPlayerController.humanPlayerInstance.transform.position, transform.position) < 20)
            {
                float flip = Random.value;
                if (flip <= accuracy && shouldShoot)
                {
                    playerController.Shoot(transform.position, transform.forward);
                }
                
                movementDirection = (HumanPlayerController.humanPlayerInstance.transform.position - transform.position)/10;
                movementDirection.y = 0;

                targetRotation = Quaternion.LookRotation(movementDirection);
            }

            else {
                // Generate new random values
                movementDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
                shouldJump = false;
                targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            shouldShoot = !shouldShoot;
        }
    }
}
