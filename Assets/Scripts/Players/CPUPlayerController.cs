using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class CPUPlayerController : MonoBehaviour
{
    private PlayerController playerController;

    private float nextChangeTime = 0.0f;
    private float changePeriod = 1.0f;

    private Vector3 movementDirection;
    private bool shouldJump;
    private Quaternion targetRotation;
    private bool shouldShoot = true;

    private float accuracy = 0.5f;

    private static int DETECTION_DISTANCE = 10;

    private PlayerModel playerModel;
    
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerModel = GetComponent<PlayerModel>();

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

        //print(movementDirection);
        playerController.SetMovement(movementDirection, shouldJump, targetRotation);

        if (Time.time > nextChangeTime)
        {
            print("updating...");
            nextChangeTime += changePeriod;

            var nearbyEnemies = Physics.OverlapSphere(transform.position, DETECTION_DISTANCE, LayerMask.GetMask("Player"));
            // Select the nearby player, if any, with the most optimal number
            // We always have one overlap (ourself)
            if (nearbyEnemies.Length <= 1)
            {
                print("No nearby enemies");
                shouldShoot = false;
                movementDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
                targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                return;
            }

            GameObject selectedTarget = null;

            var biggerEnemies = new List<GameObject>();
            var smallerEnemies = new List<GameObject>();

            foreach (var enemy in nearbyEnemies)
            {
                if (enemy.gameObject.transform == transform)
                {
                    // This is the same player
                    print("skipping");
                    continue;
                }
                var model = enemy.gameObject.GetComponent<PlayerModel>();
                if (model.level == playerModel.level)
                {
                    // Shoot at this guy
                    selectedTarget = enemy.gameObject;
                    break;
                } else if (model.level < playerModel.level)
                {
                    smallerEnemies.Add(enemy.gameObject);
                } else
                {
                    biggerEnemies.Add(enemy.gameObject);
                }
            }

            if (selectedTarget == null)
            {
                if (biggerEnemies.Count > 0)
                {
                    selectedTarget = biggerEnemies[Random.Range(0, biggerEnemies.Count)];
                }

                if (smallerEnemies.Count > 0)
                {
                    selectedTarget = smallerEnemies[Random.Range(0, biggerEnemies.Count)];
                }

                if (selectedTarget == null) {
                    shouldShoot = false;
                    movementDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
                    targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    return;
                }
            }

            float flip = Random.value;

            playerController.Shoot(transform.position, selectedTarget.transform.position);

            // Move towards the player you're shooting at
            movementDirection = (selectedTarget.transform.position - transform.position) / 10;
            movementDirection.y = 0;
            targetRotation = Quaternion.LookRotation(movementDirection);
        }
    }
}
