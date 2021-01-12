using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int Money { get; set; } = 40;
    public int Reputation { get; set; } = 0;
    public int ReputationMax { get { return ReputationLevel * 60; } set { } }
    public int ReputationLevel { get; set; } = 1;
    public List<GameObject> rooms;

    public Head head;
    //  public int RoomsCount = 1;

    public List<GameObject> workers;

    public List<GameObject> Documents;

    public int CountWorker
    {
        get
        {
            if (workers != null)
                return workers.Count;
            return 0;
        }
        set { }
    }


    public void RepLevelUP()
    {
        if (Reputation >= ReputationMax)
        {
            Reputation = 0;
            ReputationLevel++;
            RoomSell(ReputationLevel);
        }
    }

    public void RoomSell(int reputationLevel)
    {
        GameObject room = GameObject.Find("Room" + reputationLevel);
        room.GetComponent<Room>().price = 200 * reputationLevel;
        room.AddComponent<BoxCollider2D>();
        room.GetComponent<BoxCollider2D>().size = room.GetComponent<RectTransform>().sizeDelta;
    }
    
    private void Start()
    {
  //      AddRoom("Room1");
    }

    public void AddRoom(string name)
    {
        rooms.Add(GameObject.Find(name));
        Destroy(rooms[rooms.Count - 1].GetComponent<BoxCollider2D>());
        SpawnClient spawn = rooms[rooms.Count - 1].GetComponent<SpawnClient>();
        spawn.flagSpawn = true;
      //  spawn.Spawn();
    }

    public void Click()
    {
        workers[0].GetComponent<Player>().DoWork();
    }

    public void HireWorker()
    {
       // workers.Add(Instantiate();
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
            for (int i = 0; i < save.documents[j].Count; i++)
            {
                workers[j].GetComponent<Human>().AcceptedJob(save.documents[j][i].NumbDocument);
                workers[j].GetComponent<Human>().documents[i].GetComponent<Document>().Enumerator=save.documents[j][i].Enumirator;
            }                    

    }
}