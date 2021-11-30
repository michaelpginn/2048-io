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
    public GameObject damagePanel;

    public Text scoreText;
    public Text healthText;

    public int score;
    public PlayerLevel level;
    
    private void Awake()
    {
        instance = this;
        score = 0;
    }

    private void Update()
    {
        level = player.GetComponent<PlayerController>().GetLevel();
        scoreText.text = "Score: " + score.ToString();
        //healthText.text = player.GetComponent<PlayerModel>().GetCurrentHealth() + "/" + player.GetComponent<PlayerModel>().GetMaxHealth();
    }

    public void UpdateScore()
    {
        print("Level is " + level);
        score += level.GetNumericalValue();
    }

    public void IncrementScore(int amount) {
        score += amount;
    }

    public void ShowDamageNumber(Vector3 position, int damage)
    {
        var screenPoint = Camera.main.WorldToScreenPoint(position) + new Vector3(0, 20, 0);
        Instantiate(damageNumberPrefab, screenPoint, Quaternion.identity, thirdPCanvas.transform);
        Instantiate(damageNumberPrefab, screenPoint, Quaternion.identity, aimCanvas.transform);
    }

    public void GotShot() {
        StartCoroutine(ShowDamage());
    }

    IEnumerator ShowDamage()
    {
        damagePanel.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        damagePanel.SetActive(false);
    }
}