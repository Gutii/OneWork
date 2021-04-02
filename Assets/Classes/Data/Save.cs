using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;

public class Save : MonoBehaviour
{
    private string filePath;    

    void Start()
    {
        filePath = Application.persistentDataPath + "/save.bit";
    }

    public static void SaveFileJSON(object obj)
    {
        File.WriteAllText(Company.path, JsonUtility.ToJson(obj));
    }

    public void Data(Company company, Company.CompanyData data)
    {
        //company.Money = data.Money;
        //company.Reputation = data.Reputation;
        //company.ReputationLevel = data.ReputationLevel;

        //if (save.RoomsOwned != null)
        //    foreach (var item in data.RoomsOwned)
        //        AddRoom(item);

        //for (int i = 2; i <= save.ReputationLevel; i++)
        //    if (rooms.Count > i - 2)
        //        if (rooms[i - 2].name.Replace("Room", "") != i.ToString())
        //            RoomSell(i);

        ////получение документов работникам
        //for (int j = 0; j < workers.Count; j++)
        //    if (workers[j].Count != 0)
        //        for (int i = 0; i < save.Documents[j].Count; i++)
        //        {
        //            //    workers[j][0].GetComponent<Human>().AcceptedJob(save.Documents[j][i].NumbDocument);
        //            workers[j][0].GetComponent<Human>().Documents[i].GetComponent<Document>().Enumerator = save.Documents[j][i].Enumirator;
        //        }

    }

}

