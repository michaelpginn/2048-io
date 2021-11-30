using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    public static EnterShop instance;
    public GameObject crosshairs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.GetComponent<PlayerModel>() || !(collider.GetComponent<PlayerModel>().playerType == PlayerType.human))
        {
            return;   
        }

        print("HIHIHIHI");
        UI_Shop.instance.Show();
        crosshairs.SetActive(false);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.GetComponent<PlayerModel>() || !(collider.GetComponent<PlayerModel>().playerType == PlayerType.human))
        {
            return;
        }

        UI_Shop.instance.Hide();
        crosshairs.SetActive(true);

    }

}
