using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{

    public static UI_Shop instance;
 
    public GameObject MainMenu;
    public GameObject crown;
    public GameObject sombrero;
    public GameObject mustache;
    public GameObject crosshairs;
    public GameObject character;
    public GameObject currentHat; 

    public Text crownText;
    public Text sombreroText;
    public Text mustacheText;
    public Text currScore;

    private PlayerModel playerModel;
    public Vector3 position; 
    
    private bool ownsCrown = false;
    private bool ownsSombrero = false;
    private bool ownsMustache = false;

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        instance = this;
        Hide();
    }
    public void Show()
    {
        crosshairs.SetActive(false);
        MainMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;


        print("opened shop");
        Time.timeScale = 0;
        HumanPlayerController.humanPlayerInstance.pause = true;
        PlayerController.inShop = true;
        print("paused time");

        if (ownsCrown) {
            crownText.text = "PUT ON CROWN";
        }
        if (ownsSombrero) {
            sombreroText.text = "PUT ON SOMBRERO";
        }
        if (ownsMustache) {
            crownText.text = "PUT ON MUSTACHE";
        }

        currScore.text = "Current Score: " + GameController.instance.score + " PTS";

    }
    public void Hide() {
        print("closed shop");
     
        MainMenu.SetActive(false);
        crosshairs.SetActive(true);
        Time.timeScale = 1;
        PlayerController.inShop = false;
        if(HumanPlayerController.instance != null)
        {
            HumanPlayerController.instance.pause = false;

        }
    }

    public void getCrown()
    {
        if (!ownsCrown) {
            if (GameController.instance.score >= 10) {
                GameController.instance.score = GameController.instance.score - 10; 
                ownsCrown = true;
            } else {
                // not enough moneys
                return;
            }
        }
        if (currentHat != null) {
            Destroy(currentHat);
        }
        position = character.transform.position+ new Vector3(0,GameController.instance.level.GetScale().y/2,0);
        currentHat=Instantiate(crown, position, Quaternion.identity);
        currentHat.transform.parent = character.transform;
        Hide();
    }

    public void getSombrero()
    {
        if (!ownsSombrero) {
            if (GameController.instance.score >= 50) {
                GameController.instance.score = GameController.instance.score - 50; 
                ownsSombrero = true;
            } else {
                // not enough moneys
                return;
            }
        }
        if (currentHat != null) {
            Destroy(currentHat);
        }
        position = character.transform.position+ new Vector3(0,GameController.instance.level.GetScale().y/2,0);
        currentHat=Instantiate(sombrero, position, Quaternion.identity);
        currentHat.transform.parent = character.transform;
        Hide();
    }

    public void getMustache()
    {
        if (!ownsMustache) {
            if (GameController.instance.score >= 100) {
                GameController.instance.score = GameController.instance.score - 100; 
                ownsCrown = true;
            } else {
                // not enough moneys
                return;
            }
        } 
        if (currentHat != null) {
            Destroy(currentHat);
        }
        position = character.transform.position+ new Vector3(0,GameController.instance.level.GetScale().y/2,0);
        currentHat=Instantiate(mustache, position, Quaternion.identity);
        currentHat.transform.parent = character.transform;
        Hide();
    }

    
}
