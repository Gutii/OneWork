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
            DestroyChildren(gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            CreateRoom(false);
            data.name = name;
            company.companyData.rooms.Add(company.rooms[company.rooms.Count - 1].GetComponent<Room>().data);
        }
    }


    public void CreateRoom(bool load)
    {
        _tables = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            company.workers.Add(company.workers.Count, null);
            if (load)
            {
                _tables[i] = i == 0 ? WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]-1], new Vector3(103, -70, -1), transform, 83, 91) :
                 WorkGameObject.CreateObject(tables[data.TableSpriteNumber[i]-1], new Vector3(-103, -70, -1), transform, 83, 91);
            }
            else
            {
                _tables[i] = i == 0 ? WorkGameObject.CreateObject(tables, new Vector3(103, -70, -1), transform, 83, 91) :
                  WorkGameObject.CreateObject(tables, new Vector3(-103, -70, -1), transform, 83, 91);
                data.TableSpriteNumber.Add(int.Parse(_tables[i].name.Replace("Table", "").Replace("(Clone)", "")));
            }
        }

        _tables[1].GetComponent<SpriteRenderer>().flipX = true;
        company.RoomAdd(name);
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
        this.data = data;
        CreateRoom(true);        
        int j = 0;

        foreach(Human.Data humen in data.worker)
        {
            Vector3 position = _tables[j].GetComponent<Position>().positionHuman;
            GameObject worker = WorkGameObject.CreateObject(company.workersPrefab[humen.id-1], position,
                gameObject.transform, 84, 87, 1);
            company.workers[company.CountWorker]=worker;            
            company.CountWorker++;
            var _worker = worker.transform.Find("Worker").gameObject.GetComponent<Human>();
            _worker.Company = company;
            _worker.Table = _tables[j];

            if (_worker.Table.GetComponent<SpriteRenderer>().flipX)
            {
                _worker.GetComponent<SpriteRenderer>().flipX = true;
                position.x = -position.x;
                worker.transform.localPosition = position;
            }
            _worker.data = humen;
            foreach (Document document in humen.documents)
            {
                _worker.AcceptedJob(document,false);
            }
            j++;
        }

        if (data.worker.Count == 2)
            company.RoomFull++;
    }

}
