using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public static GameController instance;
    public GameObject player;

    public Text scoreText;
    public Text healthText;
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        var level = player.GetComponent<PlayerController>().GetLevel();
        scoreText.text = "Level: " + level.GetNumericalValue().ToString();
        healthText.text = player.GetComponent<PlayerModel>().GetCurrentHealth() + "/" + player.GetComponent<PlayerModel>().GetMaxHealth();
    }
}