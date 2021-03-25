using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Company : MonoBehaviour
{
    public CompanyData companyData;
   
    public int ReputationMax { get { return companyData.reputationLevel * 60; } set { } }
    
    public GameObject warningPrefab;
    public GameObject boxesPrefab;
    public GameObject moneyPrefab;
    [HideInInspector] public List<GameObject> rooms;
    private int RoomFull { get; set; } = 0;
    public SoundManager soundManager;
    public Head head;
    //  public int RoomsCount = 1;

    public GameObject playerPrefab;
    [HideInInspector] public GameObject player;
    public GameObject[] workersPrefab;
    [HideInInspector] public Dictionary<int, GameObject> workers = new Dictionary<int, GameObject>();
    public List<Sprite> documents;

    static public string path;
    public void RepLevelUP()
    {
        if (companyData.reputation >= ReputationMax)
        {
            companyData.reputation = 0;
            companyData.reputationLevel++;
            if (companyData.reputationLevel < 13)
                RoomSell();
            else
                Debug.Log("You WIN!");
        }
    }

    public void RoomSell()
    {
        GameObject room = GameObject.Find("Room" + companyData.reputationLevel);
        room.GetComponent<Room>().price = 200 * companyData.reputationLevel;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;

        WorkGameObject.CreateObject(boxesPrefab, new Vector3(0, -70, 0),room.transform, 1, 1, 1);

    }

    public void RoomSell(int reputationlevel)
    {
        GameObject room = GameObject.Find("Room" + reputationlevel);
        room.GetComponent<Room>().price = 200 * reputationlevel;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;
    }

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
      //  companyData = Load.LoadingJSON<CompanyData>();
        if (companyData == null)
            companyData = new CompanyData();
        //RoomSell();   
        GameObject.Find("Room1").GetComponent<Room>().BuyRoom();
    }

    private void OnApplicationQuit()
    {
        Save.SaveFileJSON(companyData);
    }

    
    public void AddRoom(string name)
    {
        rooms.Add(GameObject.Find(name));
        if (workers[0] == null)
        {
            workers[0] = WorkGameObject.CreateObject(playerPrefab, rooms[0].GetComponent<Room>()._tables[0].GetComponent<Position>().positionHuman,
                rooms[0].transform, 84, 87, 1);
            workers[0].transform.Find("Worker").GetComponent<Player>().Table = rooms[0].GetComponent<Room>()._tables[0];
            companyData.CountWorker++;
            player = workers[0].transform.Find("Worker").gameObject;
            head.GetComponent<Head>().player = workers[0].transform.Find("Worker").GetComponent<Player>();
            companyData.workers.Add(player.GetComponent<Player>().data);

        }
        Destroy(rooms[rooms.Count - 1].GetComponent<BoxCollider2D>());
        SpawnClient spawn = rooms[rooms.Count - 1].GetComponent<SpawnClient>();
        spawn.flagSpawn = true;        
           
    }

    
    public void Click()
    {
        if(workers.Count!=0)
        if (workers.Count != 0)
            player.GetComponent<Player>().DoWork();
    }

    public void HireWorker(int buy)
    {
      if(RoomFull < rooms.Count)
      {

            if(companyData.CountWorker < workers.Count)
            {
                    companyData.money -= buy; 
                    GameObject table = rooms[RoomFull].GetComponent<Room>()._tables[companyData.CountWorker % 2];
                    
                    Vector3 position = table.GetComponent<Position>().positionHuman; 
                    workers[companyData.CountWorker] = WorkGameObject.CreateObject(workersPrefab, position, rooms[RoomFull].transform, 84, 87, 1);
                    GameObject worker = workers[companyData.CountWorker].transform.Find("Worker").gameObject;
                    worker.GetComponent<Worker>().Table = table;
                    if (table.GetComponent<SpriteRenderer>().flipX)
                    {
                        worker.GetComponent<SpriteRenderer>().flipX = true;
                        position.x = -position.x;
                        workers[companyData.CountWorker].transform.localPosition = position;
                    }
                    companyData.CountWorker++;
                    if (companyData.CountWorker % 2 == 0)
                        RoomFull++;                
            }
      }
    }


    [System.Serializable]
    public class CompanyData
    {
        public int money=0;
        public int reputation;
        public int reputationLevel = 1;
        public int roomsCount;

        public List<Human.Data> workers = new List<Human.Data>();
        //public List<List<Document>> documents;

        public int CountWorker { get; set; } = 0;

    }

   
}