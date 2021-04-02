using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject[] tables;
    [HideInInspector] public GameObject[] _tables;

    private Company company;
    public int price;
    public GameObject panelsellroom;
    private GameObject currentpanel;
    public Data data = new Data();
    

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if (currentpanel == null)
        {
            DestroyChildren(gameObject);
            currentpanel = Instantiate(panelsellroom);
            var Price = gameObject.transform.Find("Price");
            currentpanel.transform.Find("Buy").gameObject.GetComponent<Button>().onClick.AddListener(BuyRoom);

            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { CancelRoom((PointerEventData)data); });
            currentpanel.GetComponent<EventTrigger>().triggers.Add(entry);

            currentpanel.transform.Find("Price").gameObject.GetComponent<Text>().text = price.ToString() + "$";
            currentpanel.transform.SetParent(transform);
            currentpanel.transform.localScale = new Vector3(1f, 1f, 1f);
            currentpanel.transform.localPosition = new Vector3(0f, 0f, -1f);
            currentpanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);


        }
    }

    public void BuyRoom()
    {
        if (company.companyData.Money >= price)
        {
            company.companyData.Money -= price;
            _tables = new GameObject[2];
            DestroyChildren(gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            for (int i = 0; i < 2; i++)
            {
                company.workers.Add(company.workers.Count,null);
                _tables[i] = i == 0 ? WorkGameObject.CreateObject(tables, new Vector3(103, -70, -1), transform, 83, 91) :
                    WorkGameObject.CreateObject(tables, new Vector3(-103, -70, -1), transform, 83, 91);
                data.TableSpriteNumber.Add(int.Parse(_tables[i].name.Replace("Table", "").Replace("(Clone)","")));
            }

            _tables[1].GetComponent<SpriteRenderer>().flipX = true;
            data.name = name;
            company.RoomAdd(name);
        }
    }

    public void BuyRoom(Data data)
    {
        DestroyChildren(gameObject);
        for (int i = 0; i < 2; i++)
        {
            company.workers.Add(company.workers.Count, null);
            _tables[i] = i == 0 ? WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]], new Vector3(103, -70, -1), transform, 83, 91) :
                WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]], new Vector3(-103, -70, -1), transform, 83, 91);
        }
    }


    public void CancelRoom(BaseEventData data)
    {
        WorkGameObject.CreateObject(company.boxesPrefab, new Vector3(0, -70, 0), transform, 1, 1, 1);
        Destroy(currentpanel);
    }

    public static void DestroyChildren(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            Destroy(gameObject.transform.GetChild(i).gameObject);
    }

    [System.Serializable]
    public class Data
    {
        public string name;
        public List<int> TableSpriteNumber = new List<int>();
        public List<Human.Data> worker = new List<Human.Data>();
    }

    public void LoadRoom(Data data)
    {
        for (int i = 0; i < 2; i++)
        {
            company.workers.Add(company.workers.Count, null);
            _tables[i] = i == 0 ? WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]], new Vector3(103, -70, -1), transform, 83, 91) :
                WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]], new Vector3(-103, -70, -1), transform, 83, 91);
            WorkGameObject.CreateObject(company.workersPrefab[data.worker[i].id], _tables[i].GetComponent<Position>().positionHuman,
                gameObject.transform, 84, 87, 1); 
        }
    }

}
