using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

    }

    private Transform container;
    private Transform shoptemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shoptemplate = container.Find("ShopItemTemp");
     //   shoptemplate.gameObject.SetActive(false); 

    } 
    private void CreateItemButton(string name)
    {
        Transform shopItemTransform = Instantiate(shoptemplate, container);
        shopItemTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().SetText(name);
    }

    private void Start()
    {
        CreateItemButton("Gun");
        CreateItemButton("Armor");
    }
}