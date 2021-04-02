using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;

public class Shop : MonoBehaviour
{
    private Company company;   

    private List<PanelObject> panelObjects = new List<PanelObject>();
    
    private class PanelObject
    {
        public GameObject textPrice;
        public GameObject level;

        public PanelObject(GameObject _textPrice, GameObject _level)
        {
            textPrice = _textPrice;
            level = _level;
        }
    }

    private void GetObjectPanel()
    {
        GameObject List = GameObject.Find("List");
        for(int i = 0; i <= List.transform.childCount-1; i++)
        {
            panelObjects.Add(new PanelObject(List.transform.GetChild(i).GetChild(1).Find("Price").gameObject, List.transform.GetChild(i).GetChild(0).Find("level").gameObject));
        }
    }

    void Start()
    {
        GetObjectPanel();
        company = GameObject.Find("Canvas").GetComponent<Company>();        
        LoadPrice();
    }

    private void LoadPrice()
    {
        Player player = null;
       // Worker worker = null;
        if (company.workers.Count != 0)
            player = company.workers[0].transform.Find("Worker").GetComponent<Player>();        
        if (player != null)
            PanelOptions(0, (player.data.Efficiency * 50).ToString(), (player.data.Efficiency).ToString());
        PanelOptions(1, (company.companyData.CountWorker * 250).ToString(), (company.companyData.CountWorker).ToString());
    }

    
    private void PanelOptions(int number, string TextPrice, string Level)
    {
        panelObjects[number].textPrice.GetComponent<Text>().text = TextPrice;
        panelObjects[number].level.GetComponent<Text>().text = "ур. " + Level;
    }

    public bool Buy(int number)
    {
        if (EnougMoney(int.Parse(panelObjects[number].textPrice.GetComponent<Text>().text)))
            return false;
        return true;
    }

    public bool EnougMoney(int Price)
    {
        if(Price>company.companyData.Money)
        return true;
        else
        return false;
    }

    public void HireWorker()
    {        
        if (Buy(1))
        {
            company.HireWorker(int.Parse(panelObjects[1].textPrice.GetComponent<Text>().text));
            LoadPrice();
        }
    }

    public void UpWorkerEfficiency()
    {
        if (company.companyData.CountWorker > 1)
            if (Buy(0))
            {
            for(int i=1; i < company.workers.Count;i++)
                    if(company.workers[i] != null)
                        company.workers[i].transform.Find("Worker").GetComponent<Worker>().data.Efficiency++;
            company.companyData.Money -= int.Parse(panelObjects[0].textPrice.GetComponent<Text>().text);
            LoadPrice();
            }
    }


    public void UpPlayerEfficiency()
    {
        if (company.workers.Count != 0)
            if (Buy(0))
            {
            company.player.GetComponent<Player>().data.Efficiency++;
            company.companyData.Money -= int.Parse(panelObjects[0].textPrice.GetComponent<Text>().text);
            LoadPrice();
            }
    }


    public void CloseMenu()
    {
        Destroy(gameObject);
        company.soundManager.PlayBackGround(SoundManager.SoundEnum.Background);
    }
}
