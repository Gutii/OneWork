using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Company company;

    [System.Serializable]
    public class PanelObject 
    {
        public GameObject textPrice;
        public GameObject Level;
    }

    public List<PanelObject> panelObjects;
    void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
        LoadPrice();
    }

    private void LoadPrice()
    {
        Player player = company.workers[0][0].GetComponent<Player>();

        panelObjects[0].textPrice.GetComponent<Text>().text = (player.efficiency * 50).ToString();
        panelObjects[0].Level.GetComponent<Text>().text = "ур. " + (player.efficiency).ToString();
    }


    public bool EnougMoney(int Price)
    {
        if(Price>company.Money)
        return true;
        else
        return false;
    }

    public void UpPlayerEfficiency()
    {        
        if (EnougMoney(int.Parse(panelObjects[0].textPrice.GetComponent<Text>().text)))
            return;

        company.workers[0][0].GetComponent<Player>().efficiency++;
        company.Money -= int.Parse(panelObjects[0].textPrice.GetComponent<Text>().text);

        LoadPrice();
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
