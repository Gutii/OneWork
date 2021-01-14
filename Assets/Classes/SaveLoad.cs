using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using System.Timers;


public class SaveLoad : MonoBehaviour
{
    private string filePath;
    public Company company;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/save.bit";
        LoadedGame(); 
       // Invoke("SaveGame", 5);        
    }

    private void SaveGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath,FileMode.Create);
        Save save = new Save();

        save.money = company.Money;
        save.reputation = company.Reputation;
        save.ReputationMax = company.ReputationMax;
        save.ReputationLevel = company.ReputationLevel;
        save.RoomsOwned = new List<string>();
        save.documents = new List<List<Save.Document>>();

        foreach (var item in company.rooms)        
            save.RoomsOwned.Add(item.name);

        for (int i = 0; i < company.workers.Count; i++)
        {
            save.documents.Add(new List<Save.Document>());
            var document = company.workers[i].GetComponent<Human>().documents;
            for (int j = 0; j < document.Count; j++)
            {
                save.documents[i].Add( new Save.Document(i,int.Parse(document[j].name.Replace("Document", "").
                    Replace("(Clone)", "")), document[j].GetComponent<Document>().Enumerator));
            }
        }

        binaryFormatter.Serialize(fileStream, save);

        fileStream.Close();
        Invoke("SaveGame", 5);
    }

    public void LoadedGame()
    {
        try
        {
            if (!(File.Exists(filePath)))
                return;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            if (fileStream.Length == 0)
                return;
            Save save = (Save)binaryFormatter.Deserialize(fileStream);

            company.LoadData(save);
            fileStream.Close();
        }
        catch(System.Exception e)
        {
            Debug.LogError("Error Loading Save " + e);
            return;
        }
    }
}

[System.Serializable]
public class Save
{
    public int money;
    public int reputation;
    public int ReputationMax;
    public int ReputationLevel;
    public List<string> RoomsOwned;
    public List<int> workers;
    public List<List<Document>> documents;


    [System.Serializable]
    public struct Document
    {
        public int Human;
        public int NumbDocument;
        public int Enumirator;

        public Document(int human, int numbDocument, int enumirator)
        {
            Human = human;
            NumbDocument = numbDocument;
            Enumirator = enumirator;
        }
    }
}