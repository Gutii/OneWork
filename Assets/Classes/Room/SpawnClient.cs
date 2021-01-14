using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    //public GameObject WarningObject;
    [SerializeField] private GameObject[] ClientArray;
    private GameObject client;
    private Company company;

    [HideInInspector] public bool flagSpawn;        
    private Vector3 rightposition = new Vector3(-44f, -70, -2);
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
                client = WorkGameObject.CreateObject(ClientArray, rightposition, transform);
                client.GetComponent<Client>().StartSpawn();
            }
        }
    }
    
}