using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Client : MonoBehaviour
{
    private Company company;

    public GameObject warningPrefab;
    private GameObject warning;
    private GameObject player;
    private Coroutine coroutine;
    Transform currentParent;
    Vector3 currentLocalPsition;

    private void Start()
    {
        company = GameObject.Find("Canvas").gameObject.GetComponent<Company>();
        if (company.workers.Count != 0)
        {
            player = company.workers[0][0];
            currentParent = player.transform.parent;
            currentLocalPsition = player.transform.localPosition;
        }
    }

    public void StartSpawn()
    {
        coroutine = StartCoroutine(SpawnChans());
    }

    public IEnumerator SpawnChans()
    {
        while (true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(2f);            
            float Chance = UnityEngine.Random.Range(0f, 100f);
            if (Chance <= 30)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                yield return new WaitForSeconds(5f);
                warning = WorkGameObject.CreateObject(warningPrefab, new Vector3(0.42f, 0.75f, 0), transform, 0.42f, 0.75f, 1f);
                warning.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
                yield return new WaitForSeconds(5f);                
                if(gameObject!=null)                    
                Destroy(gameObject);
                Destroy(warning);               
            }
        }
    }

   

    private void OnMouseDown()
    {
        if (player.GetComponent<Player>().documents.Count <= 10)
        {
            if (warning != null)
                Destroy(warning);
            StopCoroutine(coroutine);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().Play("ClientContract");
            StartCoroutine(GettingJob(player));
        }
    }

    public IEnumerator GettingJob(GameObject worker)
    {  
        
        worker.transform.parent = gameObject.transform.parent;
        worker.transform.localPosition = gameObject.transform.localPosition;

        worker.GetComponent<Animator>().Play("WorkerContract");
        worker.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            worker.transform.localPosition += new Vector3(-40f, 0, 0);
        }
        else
        {
            worker.transform.localPosition += new Vector3(40f, 0, 0); 
        }
                

        yield return new WaitForSeconds(0.5f);
        worker.GetComponent<Human>().AcceptedJob(0);
        Destroy(gameObject);
        Destroy(warning);
        worker.transform.parent = currentParent;
        worker.transform.localPosition = currentLocalPsition;
        worker.GetComponent<Animator>().Play("Worker");

    }

}