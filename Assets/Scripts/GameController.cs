using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public static GameController instance;
    public PlayerController playerController;

    // Outlets
    public Text scoreText;
    

    // Methods
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = "Level: " + playerController.GetLevel().GetNumericalValue().ToString();
    }
}