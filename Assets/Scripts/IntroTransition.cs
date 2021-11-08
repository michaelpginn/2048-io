using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroTransition : MonoBehaviour
{
    
    // Outlets
    public Text timerText;
    
    //State Tracking
    public int timer = 20;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = ":" + timer.ToString();
        
        StartCoroutine("UpdateTimer");
        StartCoroutine("LoadMainScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator LoadMainScene()
    {
        // Wait
        yield return new WaitForSeconds(timer);

        // Load Next Scene
        SceneManager.LoadScene("MainScene");
    }
    
    IEnumerator UpdateTimer()
    {
        // Wait
        yield return new WaitForSeconds(1);
    
        // Update Counter
        timer -= 1;
        
        timerText.text = ":";
        if (timer < 10)
        {
            timerText.text += "0";
        }

        timerText.text += timer.ToString();

        if (timer > 0)
        {
            StartCoroutine("UpdateTimer");
        }
    }
}
