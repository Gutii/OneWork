using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Company company;
    public GameObject ShopMenu;

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }

    public void MenuShopOpen()
    {
        WorkGameObject.CreatePanel(ShopMenu);
    }



}
