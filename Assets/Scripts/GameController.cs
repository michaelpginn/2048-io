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
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = "Level: " + player.GetComponent<PlayerController>().GetLevel().GetNumericalValue().ToString();
    }
}