using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public static GameController instance;
    
    // Outlets
    public Text scoreText;

    
    // State Tracking
    public static int enemies = 7;
    private float floorSize = 250f;
    public static bool spawnEnemy = false;
    public static PlayerLevel currentLevel = PlayerLevel.Level2;
    public int score;


    // Methods
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
      //  if (spawnEnemy)
      //  {
       //     SpawnCPU();
       //     spawnEnemy = false;
       // }
       UpdateDisplay();
       
    }

    void UpdateDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateScore()
    {
        score += (int)Mathf.Pow(2f,((int) currentLevel)+1f);
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