using System.Collections.Generic;
using UnityEngine;


public abstract class Human : MonoBehaviour
{
    public abstract Company Company { get; set; }
    public abstract bool Fatigue { get; set; } 
    public abstract GameObject Table { get; set; }

    public abstract List<GameObject> Documents { get; set; }
    public abstract Data data { get; set; }

    [System.Serializable]
    public class Data
    {
        public int id;
        public int Efficiency = 1;
        public List<Document> documents = new List<Document>();
    }


    public virtual void AcceptedJob(Document Document, bool load)
    {
        GameObject document = new GameObject();
        document.AddComponent<SpriteRenderer>();
        document.GetComponent<SpriteRenderer>().sprite = Company.documents[0];
        Documents.Add(document);   
        
        if(load)
        {
            Company.soundManager.PlaySound(SoundManager.SoundEnum.AcceptJob, gameObject);
            data.documents.Add(Document);
        }
        
        GameObject currentdocument = Documents[Documents.Count - 1];
        currentdocument.transform.SetParent(Table.transform);       
        Documents[Documents.Count-1] = WorkGameObject.DocumentTransform(Documents, Table.GetComponent<Position>().DocumentPosition(), gameObject.GetComponent<SpriteRenderer>().flipX);
        
    }

    public void DoWork()
    {
        try
        {
            if (Documents != null)
                if (Documents.Count != 0)
                {

                    var documentObject = Documents[Documents.Count - 1];
                    var document = data.documents[data.documents.Count - 1];
                    document.enumerator += data.Efficiency;
                    if (document.work <= document.enumerator)
                    {
                        if (System.Int64.Parse((Company.companyData.Money ).ToString()) + document._money > int.MaxValue)
                            Company.companyData.Money = int.MaxValue;
                        else
                        Company.companyData.Money += document._money;
                        Company.companyData.Reputation += document._reputation;
                        Company.soundManager.PlaySound(SoundManager.SoundEnum.Money, gameObject);
                        Destroy(documentObject);
                        Documents.Remove(documentObject);
                        data.documents.Remove(document);
                        if (gameObject.GetComponent<SpriteRenderer>().flipX)
                            WorkGameObject.CreateObject(Company.MoneyPrefab, new Vector3(0.25f, 0.6f, -2f), transform, 0.0119696427f, 0.0114968587f, 1.13919187f);
                        else
                            WorkGameObject.CreateObject(Company.MoneyPrefab, new Vector3(-0.25f, 0.6f, -2f), transform, 0.0119696427f, 0.0114968587f, 1.13919187f);
                    }
                    if (!(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(StateAnimation.StateWorker.Contract)))
                    {
                        gameObject.GetComponent<Animator>().Play(StateAnimation.StateWorker.DoWork);
                        gameObject.GetComponent<Animator>().Play(StateAnimation.StateWorker.Wait);
                    }

                }
        }
        catch(System.OverflowException e)
        {
            Company.companyData.Money = int.MaxValue;
            Debug.Log("DoWork overflow: " + e.ToString());            
        }
    }
   
}