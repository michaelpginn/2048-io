using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{

    public static UI_Shop instance;
    public GameObject MainMenu;
    //public GameObject Hat1;
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
       
        MainMenu.SetActive(true);
        print("HIHIHIHI SHOP OPENED WHOOO");
    }
    public void Hide()
        {
            gameObject.SetActive(false);
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

    }

}
