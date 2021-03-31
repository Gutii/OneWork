using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    //public GameObject WarningObject;
    [SerializeField] private GameObject[] clientArray;
    private GameObject client;

    [HideInInspector] public bool flagSpawn;        
    private Vector3 rightposition = new Vector3(-44f, -70, -2);


    private void Update()
    {
        if(flagSpawn)
        {
            if (client == null)
            {
                client = WorkGameObject.CreateObject(clientArray, rightposition, transform);
                client.GetComponent<Client>().Spawn();
            }
        }
    }
    
}