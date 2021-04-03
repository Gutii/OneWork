using System.Collections.Generic;
using System.IO;
using UnityEngine;

using System;

public class Company : MonoBehaviour
{
    public CompanyData companyData;

    public int ReputationMax { get { return companyData.ReputationLevel * 60; } set { } }

    public GameObject warningPrefab;
    public GameObject boxesPrefab;
    public GameObject MoneyPrefab;
    [HideInInspector] public List<GameObject> rooms;
    [HideInInspector] public int RoomFull { get; set; } = 0;
    public int CountWorker { get; set; } = 0;
    public SoundManager soundManager;
    public Head head;
    //  public int RoomsCount = 1;

    [HideInInspector] public GameObject player;
    public GameObject[] workersPrefab;
    [HideInInspector] public Dictionary<int, GameObject> workers = new Dictionary<int, GameObject>();
    public List<Sprite> documents;

    static public string path;
    public void RepLevelUP()
    {
        if (companyData.Reputation >= ReputationMax)
        {
            companyData.Reputation = 0;
            companyData.ReputationLevel++;
            if (companyData.ReputationLevel < 13)
                RoomSell();
            else
                Debug.Log("You WIN!");
        }
    }

    public void RoomSell()
    {
        GameObject room = GameObject.Find("Room" + companyData.ReputationLevel);
        room.GetComponent<Room>().price = 200 * companyData.ReputationLevel;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;

        WorkGameObject.CreateObject(boxesPrefab, new Vector3(0, -70, 0), room.transform, 1, 1, 1);

    }

    public void RoomSell(int iterator)
    {
        GameObject room = GameObject.Find("Room" + iterator);
        room.GetComponent<Room>().price = 200 * iterator;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;

        WorkGameObject.CreateObject(boxesPrefab, new Vector3(0, -70, 0), room.transform, 1, 1, 1);
    }

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        companyData = Load.LoadingJSON<CompanyData>();
        if (companyData == null)
        {
            companyData = new CompanyData();
            GameObject.Find("Room1").GetComponent<Room>().BuyRoom();
            AddPlayer();
        }
        else
        {
            CompanyLoadData();
        }

    }

    private void OnApplicationQuit()
    {
        Save.SaveFileJSON(companyData);
    }


    public void RoomAdd(string name)
    {
        rooms.Add(GameObject.Find(name));
        Destroy(rooms[rooms.Count - 1].GetComponent<BoxCollider2D>());
        SpawnClient spawn = rooms[rooms.Count - 1].GetComponent<SpawnClient>();
        spawn.flagSpawn = true;

    }

    public void AddPlayer()
    {
        workers[0] = WorkGameObject.CreateObject(workersPrefab[0], rooms[0].GetComponent<Room>()._tables[0].GetComponent<Position>().positionHuman,
                rooms[0].transform, 84, 87, 1);
        player = workers[0].transform.Find("Worker").gameObject;
        player.GetComponent<Player>().Table = rooms[0].GetComponent<Room>()._tables[0];
        player.GetComponent<Player>().data.id = int.Parse(workers[0].name.Replace("(Clone)", "").Replace("Worker", ""));
        CountWorker++;
        head.GetComponent<Head>().player = workers[0].transform.Find("Worker").GetComponent<Player>();
        companyData.rooms[0].worker.Add(player.GetComponent<Player>().data);
    }

    public void Click()
    {
        if (workers.Count != 0)
            player.GetComponent<Player>().DoWork();
    }

    public void HireWorker(int buy)
    {
        if (RoomFull < rooms.Count)
        {

            if (CountWorker < workers.Count)
            {
                companyData.Money -= buy;
                GameObject table = rooms[RoomFull].GetComponent<Room>()._tables[CountWorker % 2];

                Vector3 position = table.GetComponent<Position>().positionHuman;
                workers[CountWorker] = WorkGameObject.CreateObject(workersPrefab[1], position, rooms[RoomFull].transform, 84, 87, 1);
                GameObject worker = workers[CountWorker].transform.Find("Worker").gameObject;
                worker.GetComponent<Worker>().data.id = int.Parse(workers[CountWorker].name.Replace("(Clone)", "").Replace("Worker", ""));
                worker.GetComponent<Worker>().Table = table;
                companyData.rooms[RoomFull].worker.Add(worker.GetComponent<Worker>().data);
                if (table.GetComponent<SpriteRenderer>().flipX)
                {
                    worker.GetComponent<SpriteRenderer>().flipX = true;
                    position.x = -position.x;
                    workers[CountWorker].transform.localPosition = position;
                }
                CountWorker++;
                if (CountWorker % 2 == 0)
                    RoomFull++;
            }
        }
    }


    [System.Serializable]
    public class CompanyData
    {
        public int Money = 0;
        public int Reputation = 0;
        public int ReputationLevel = 1;

        public List<Room.Data> rooms = new List<Room.Data>();

    }

    public void CompanyLoadData()
    {
        foreach (Room.Data room in companyData.rooms)
        {
            GameObject.Find(room.name).GetComponent<Room>().LoadRoom(room);
            if (player == null)
                player = workers[0].transform.Find("Worker").gameObject;
        }

        for (int i = 1; i <= companyData.ReputationLevel; i++) //checking rooms for sell
        {
            if(CheckRoomSell(i))
            RoomSell(i);
        }
    }

    public bool CheckRoomSell(int iterator)
    {
            for (int j = 0; rooms.Count > j;j++)
            {
                if (int.Parse(rooms[j].name.Replace("Room", "")) == iterator)
                    return false;
            }
        return true;
    }
   
}