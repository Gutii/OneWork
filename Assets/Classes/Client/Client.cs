using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Client : MonoBehaviour
{
    private Company company;
    
    [HideInInspector] public float ChansSpawn { get; set; } = 80f; 
    private GameObject warningPrefab;
    private GameObject warning = null;
    private GameObject player;
    private Coroutine coroutine;
    private Transform currentParent;
    private Vector3 currentLocalPsition;
    public DocumentRange documentRange;  
    private void Start()
    {
        company = GameObject.Find("Canvas").gameObject.GetComponent<Company>();
        if (company.workers.Count != 0)
        {
            player = company.workers[0];
            currentParent = player.transform.parent;
            currentLocalPsition = player.transform.localPosition;
        }
        warningPrefab = company.warningPrefab;
    }

    public void Spawn()
    {
        coroutine = StartCoroutine(SpawnChans());
    }

    public IEnumerator SpawnChans()
    {
        while (true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(2.5f);            
            float Chance = UnityEngine.Random.Range(0f, 100f);
            if (Chance >= ChansSpawn)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                yield return new WaitForSeconds(5f);
                warning = WorkGameObject.CreateObject(warningPrefab, new Vector3(0.42f, 0.75f, 0), transform);
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
        if (player.transform.Find("Worker").GetComponent<Player>().Documents.Count <= 10)
        {
            if (warning != null)
                Destroy(warning);
            StopCoroutine(coroutine);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().Play(StateAnimation.StateWorker.Wait);
            StartCoroutine(GettingJob(player));
        }
    }

    public IEnumerator GettingJob(GameObject worker)
    {  
        
        worker.transform.parent = gameObject.transform.parent;
        worker.transform.localPosition = gameObject.transform.localPosition;
        Vector3 scale = worker.transform.localScale;
        worker.transform.localScale = gameObject.transform.localScale;
        GameObject workerAnimation = worker.transform.Find("Worker").gameObject;
        workerAnimation.GetComponent<Animator>().Play(StateAnimation.StateWorker.Contract);
        workerAnimation.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            worker.transform.localPosition += new Vector3(-40f, 0, 0);
        }
        else
        {
            worker.transform.localPosition += new Vector3(40f, 0, 0); 
        }
        
        Document document = new Document((int)UnityEngine.Random.Range(documentRange.MoneyMin, documentRange.MoneyMax),
            (int)UnityEngine.Random.Range(documentRange.ReputationMin, documentRange.ReputationMax));
        gameObject.GetComponent<Animator>().Play(StateAnimation.StateWorker.Contract);
        yield return new WaitForSeconds(0.5f);
        workerAnimation.GetComponent<Human>().AcceptedJob(document);
        Destroy(gameObject);
        Destroy(warning);
        worker.transform.parent = currentParent;
        worker.transform.localPosition = currentLocalPsition;
        worker.transform.localScale = scale;
        workerAnimation.GetComponent<Animator>().Play(StateAnimation.StateWorker.Wait);

    }

}