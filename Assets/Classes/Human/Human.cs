﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Human : MonoBehaviour
{
    public abstract Company company { get; set; }
    public abstract int efficiency { get; set; }
    public abstract bool fatigue { get; set; } 
    public abstract GameObject Table { get; set; }

    public abstract List<GameObject> documents { get; set; }

    
    public virtual void AcceptedJob(int Document, int DocumentEnumerator = 0)
    {
        documents.Add(Instantiate(company.Documents[Document]));
        GameObject currentdocument = documents[documents.Count - 1];
        currentdocument.transform.SetParent(Table.transform);
        Vector3 position = Table.GetComponent<Position>().DocumentPosition();
        currentdocument.GetComponent<Document>().Enumerator = DocumentEnumerator;
        if (documents.Count > 4)
        {
            currentdocument.transform.localScale = new Vector3(0.93f, 1.58f, 1);
            if (documents.Count == 5)
                if(gameObject.GetComponent<SpriteRenderer>().flipX)
                    currentdocument.transform.localPosition = new Vector3(position.x - 0.22f, position.y, 0);
                else
                currentdocument.transform.localPosition = new Vector3(position.x+0.22f, position.y, 0);
            else
                if (gameObject.GetComponent<SpriteRenderer>().flipX)
                currentdocument.transform.localPosition = documents.Count > 1 ?
                    new Vector3(position.x - 0.22f, 0.05f + documents[documents.Count - 2].transform.localPosition.y, 0) : new Vector3(position.x - 0.22f, position.y, 0);
            else
                currentdocument.transform.localPosition = documents.Count > 1 ?
                     new Vector3(position.x + 0.22f, 0.05f + documents[documents.Count - 2].transform.localPosition.y, 0) : new Vector3(position.x + 0.22f, position.y, 0);

        }
        else
        {
            currentdocument.transform.localScale = new Vector3(1.29f, 2.18f, 1);            
                currentdocument.transform.localPosition = documents.Count > 1 ?
                new Vector3(position.x, 0.07f + documents[documents.Count - 2].transform.localPosition.y, -1) : position;
        }
    }

    public void DoWork()
    {
        if (documents != null)
            if (documents.Count != 0)
            {
                
                var documentObject = documents[documents.Count - 1];
                var document = documentObject.GetComponent<Document>();
                document.Enumerator += efficiency;
                if (document.Work <= document.Enumerator)
                {
                    company.Money += document.Money;
                    company.Reputation += document.Reputation;                    
                    Destroy(documentObject);
                    documents.Remove(documentObject);
                }
                    
            }
    }

   
}