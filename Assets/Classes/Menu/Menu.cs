using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Company company;
    public GameObject shopMenu;
    private SoundManager soundManager;

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
        soundManager = company.soundManager;
    }

    public void MenuShopOpen()
    {
        if (company.rooms.Count > 0)
        {
            WorkGameObject.CreatePanel(shopMenu);
            soundManager.PlayBackGround(SoundManager.SoundEnum.BackgroundShop);
        }
    }

    public void CloseMenu()
    {
        soundManager.PlayBackGround(SoundManager.SoundEnum.Background);
        Destroy(gameObject);
    }
}
