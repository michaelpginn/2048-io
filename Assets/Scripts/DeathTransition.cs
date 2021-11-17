using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTransition : MonoBehaviour
{
    public void Respawn() {
        print("skeet");
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToIntro() {
        SceneManager.LoadScene("IntroScene");
    }
}
