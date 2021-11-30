using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{

    public static UI_Shop instance;
 
    public GameObject MainMenu;
    public GameObject Hat1;
    public GameObject crosshairs;
    public CursorMode cursorMode;
    //public GameObject Hat2;
    //public GameObject Hat3;
    //public GameObject Hat4;
    // Start is called before the first frame update






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
        cursorMode = CursorMode.Auto;

    print("HIHIHIHI SHOP OPENED WHOOO");
        Time.timeScale = 0;
        HumanPlayerController.instance.pause = true;
        

      
    }
    public void Hide()
        {
        crosshairs.SetActive(true);
            gameObject.SetActive(false);
             Time.timeScale = 1;
        if(HumanPlayerController.instance != null)
        {
            HumanPlayerController.instance.pause = false;

        }
    }


    void goTo(GameObject menu)
    {
        MainMenu.SetActive(false);
     //   Hat1.SetActive(false);
     //   Hat2.SetActive(false);
     //   Hat3.SetActive(false);
     //   Hat4.SetActive(false);

        menu.SetActive(true);

    }

    public void mainMenu()
    {
        goTo(MainMenu);
    }
    //public void hat1()
    //{
    //    goTo(Hat1);
    //}
    //public void hat2()
    //{
    //    goTo(Hat2);
    //}
    //public void hat3()
    //{
    //    goTo(Hat3);
    //}
    //public void hat4()
    //{
    //    goTo(Hat4);
    //}

    public void addHat1()
    {
        Instantiate(Hat1, new Vector3(0, 0, 0), Quaternion.identity);
        print("hat 1 added");
    }

    
}
