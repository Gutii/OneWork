using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int Money { get; set; } = 240;
    public int Reputation { get; set; } = 0;
    public int ReputationMax { get { return ReputationLevel * 60; } set { } }
    public int ReputationLevel { get; set; } = 1;
    public GameObject Boxes;
    public List<GameObject> rooms;
    private int roomFull = 0;

    public Head head;
    //  public int RoomsCount = 1;

    public GameObject Player;
    public GameObject[] workersPrefab;
    [HideInInspector] public List<List<GameObject>> workers = new List<List<GameObject>>();
    public List<GameObject> Documents;


    public int CountWorker { get; set; } = 0;


    public void RepLevelUP()
    {
        if (Reputation >= ReputationMax)
        {
            Reputation = 0;
            ReputationLevel++;
            RoomSell();
        }
    }

    public void RoomSell()
    {
        GameObject room = GameObject.Find("Room" + ReputationLevel);
        room.GetComponent<Room>().price = 200 * ReputationLevel;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;

        WorkGameObject.CreateObject(Boxes, new Vector3(0, -70, 0),room.transform, 1, 1, 1);

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
        RoomSell();
    }

    public void AddRoom(string name)
    {
        rooms.Add(GameObject.Find(name));
        Destroy(rooms[rooms.Count - 1].GetComponent<BoxCollider2D>());
        SpawnClient spawn = rooms[rooms.Count - 1].GetComponent<SpawnClient>();
        spawn.flagSpawn = true; 
        if (workers[0].Count == 0)
        {
            workers[0].Add(WorkGameObject.CreateObject(Player, rooms[0].GetComponent<Room>().Table[0].GetComponent<Position>().positionHuman,
                rooms[0].transform, 84, 87, 1));
            workers[0][0].GetComponent<Player>().Table = rooms[0].GetComponent<Room>().Table[0];
            CountWorker++;
            head.GetComponent<Head>().player = workers[0][0].GetComponent<Player>();
        }
           
    }

    
    public void Click()
    {
        if(workers.Count!=0)
        if (workers[0].Count != 0)
            workers[0][0].GetComponent<Player>().DoWork();
    }

    public void HireWorker()
    {
      if(roomFull<rooms.Count)
      {

            if(CountWorker < workers.Count)
            {
                if(workers[CountWorker].Count == 0)
                {
                    GameObject table = rooms[roomFull].GetComponent<Room>().Table[CountWorker % 2];
                    
                    Vector3 position = table.GetComponent<Position>().positionHuman; 
                    workers[CountWorker].Add(WorkGameObject.CreateObject(workersPrefab, position, rooms[roomFull].transform, 84, 87, 1));
                    workers[CountWorker][0].GetComponent<Worker>().Table = table;
                    if (table.GetComponent<SpriteRenderer>().flipX)
                    {
                        workers[CountWorker][0].GetComponent<SpriteRenderer>().flipX = true;
                        position.x = -position.x;
                        workers[CountWorker][0].transform.localPosition = position;
                    }
                    CountWorker++;
                    if (CountWorker % 2 == 0)
                        roomFull++;
                }
            }
      }
    }


    public void LoadData(Save save)
    {
        Money = save.money;
        Reputation = save.reputation;
        ReputationMax = save.ReputationMax;
        ReputationLevel = save.ReputationLevel;

        if(save.RoomsOwned!=null)
        foreach (var item in save.RoomsOwned)
            AddRoom(item);

        for (int i = 2; i <= save.ReputationLevel; i++)
            if (rooms.Count > i - 2)
                if (rooms[i - 2].name.Replace("Room", "") != i.ToString())
                    RoomSell(i);

        //получение документов работникам
        for (int j = 0; j < workers.Count; j++)
            if(workers[j].Count!=0)
            for (int i = 0; i < save.documents[j].Count; i++)
            {
                workers[j][0].GetComponent<Human>().AcceptedJob(save.documents[j][i].NumbDocument);
                workers[j][0].GetComponent<Human>().documents[i].GetComponent<Document>().Enumerator=save.documents[j][i].Enumirator;
            }                    

    }
}