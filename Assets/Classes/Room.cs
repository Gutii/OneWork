using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    private Company company;
    public int price;
    public GameObject panelsellroom;
    private GameObject panel;

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }

    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (panel == null)
        {
            panel = Instantiate(panelsellroom);
            var Price = gameObject.transform.Find("Price");
            panel.transform.Find("Buy").gameObject.GetComponent<Button>().onClick.AddListener(BuyRoom);
            
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { CancelRoom((PointerEventData)data); });
            panel.GetComponent<EventTrigger>().triggers.Add(entry);

            panel.transform.Find("Price").gameObject.GetComponent<Text>().text = price.ToString()+"$";
            panel.transform.SetParent(transform);
            panel.transform.localScale = new Vector3(1f,1f,1f);
            panel.transform.localPosition = new Vector3(0f, 0f, -1f);
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,0f);
        }
    }

    public void BuyRoom()
    {
        if(company.Money >= price)
        {
            company.Money -= price;
            company.AddRoom(name);
            Destroy(panel);
            Destroy(GetComponent<BoxCollider2D>());
        }
        
    }

    public void CancelRoom(BaseEventData data)
    {
        Destroy(panel);
    }

}
