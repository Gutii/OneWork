﻿using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int Money { get; set; } = 240;
    public int Reputation { get; set; } = 0;
    public int ReputationMax { get { return ReputationLevel * 60; } set { } }
    public int ReputationLevel { get; set; } = 1;
    public GameObject Boxes;
    public List<GameObject> rooms;

    public Head head;
    //  public int RoomsCount = 1;

    public GameObject[] workersPrefab;
    [HideInInspector] public List<GameObject> workers;

    public List<GameObject> Documents;
  

    public int CountWorker
    {
        get
        {
            if (workers != null)
                return workers.Count;
            return 0;
        }
    }


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

        var box = Instantiate(Boxes);
        box.transform.SetParent(room.transform);
        box.transform.localScale = new Vector3(1, 1, 1);
        box.transform.localPosition = new Vector3(0, -150, 0);       

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
    }

    public void Click()
    {
        if(CountWorker != 0)
        workers[0].GetComponent<Player>().DoWork();
    }

    public void HireWorker()
    {
      // workers.Add(WorkGameObject.CreateObject();
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