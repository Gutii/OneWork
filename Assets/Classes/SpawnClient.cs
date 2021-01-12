using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    public GameObject WarningObject;
    [SerializeField] private GameObject[] ClientArray;
    private GameObject client;
    private Company company;

    [HideInInspector] public bool flagSpawn;        
    private Vector3 rightposition = new Vector3(-44f, -68f, -2);
    private Vector3 ScaleClient = new Vector3(100f, 100f, 1);
    private void Start()
    {     
        company = GameObject.Find("Canvas").GetComponent<Company>();
       // Spawn();
    }



    private void Update()
    {
        if(flagSpawn)
        {
            if (client == null)
            {
                int random = Random.Range(0, ClientArray.Length);

                client = Instantiate(ClientArray[random]);
                client.transform.SetParent(transform);
                client.transform.localScale = ScaleClient;
                client.transform.localPosition = rightposition;
                client.GetComponent<Client>().StartSpawn(WarningObject);
            }
        }
    }
    
}