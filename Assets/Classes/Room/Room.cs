using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject[] Tables;
    [HideInInspector] public GameObject[] Table;

    [HideInInspector] public int workPlaces = 0;
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
            panel.transform.localPosition = new Vector3(0f, 0f, -5f);
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,0f);


        }
    }

    public void BuyRoom()
    {
        if(company.Money >= price)
        {
            company.Money -= price;
            company.AddRoom(name);
            Table = new GameObject[2];
            DestroyChildren(gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            for (int i = 0; i < 2; i++)
            {
                company.workers.Add(new List<GameObject>());
                Table[i] = i == 0 ? WorkGameObject.CreateObject(Tables, new Vector3(103, -70, -1), transform, 83, 91) :
                    WorkGameObject.CreateObject(Tables, new Vector3(-103, -70, -1), transform, 83, 91);
            }

            Table[1].GetComponent<SpriteRenderer>().flipX = true;
            
        }        
    }

   


    public void CancelRoom(BaseEventData data)
    {
        Destroy(panel);
    }

    public static void DestroyChildren(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            Destroy(gameObject.transform.GetChild(i).gameObject);
    }

}
