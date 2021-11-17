using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
    public GameObject damageNumberPrefab;

    public Canvas thirdPCanvas;
    public Canvas aimCanvas;

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

    public void ShowDamageNumber(Vector3 position, int damage)
    {
        var screenPoint = Camera.main.WorldToScreenPoint(position) + new Vector3(0, 20, 0);
        Instantiate(damageNumberPrefab, screenPoint, Quaternion.identity, thirdPCanvas.transform);
        Instantiate(damageNumberPrefab, screenPoint, Quaternion.identity, aimCanvas.transform);
    }
}