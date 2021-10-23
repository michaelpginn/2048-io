using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int enemies = 7;
    private float floorSize = 250f;
    public static bool spawnEnemy = false;
    public static PlayerLevel currentLevel = PlayerLevel.Level2;
   

    // Methods
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
      //  if (spawnEnemy)
      //  {
       //     SpawnCPU();
       //     spawnEnemy = false;
       // }
    }

    void SpawnCPU()
    {
        // Pick random spawn points and prefabs
        // Vector3 randomSpawnPoint = new Vector3(Random.Range(0f, floorSize), 1.5f, Random.Range(0f, floorSize));

        // GameObject newCPU = GameObject.Instantiate(PlayerController.cpu, randomSpawnPoint, Quaternion.identity);
        // newCPU.GetComponent<CPUPlayerController>().SetLevel(currentLevel);
        // Instantiate(PlayerController.cpu, randomSpawnPoint, Quaternion.identity);
        
        // enemies += 1;
    }

   
}