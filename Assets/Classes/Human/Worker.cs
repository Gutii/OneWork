using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Worker : Human
{
    public override Company company { get; set; }
    public override int efficiency { get; set; } = 1;

    //  public override int fatigue { get; set; }
    public override List<GameObject> documents { get; set; } = new List<GameObject>();
    public override bool fatigue { get; set; }
    public override GameObject Table { get; set; }
    private bool flagWork;
   

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }

    private void Update()
    {
        if(!flagWork)
        {
            flagWork = true;
            StartCoroutine(Work());
        }
    }

    private void OnMouseDown()
    {
        List<GameObject> playerDocuments = company.workers[0][0].GetComponent<Player>().documents;
        if (playerDocuments.Count != 0 && documents.Count <= 10)
        {
            AcceptedJob(int.Parse(playerDocuments[playerDocuments.Count - 1].name.Replace("Document", "").Replace("(Clone)", "")), playerDocuments[playerDocuments.Count - 1].GetComponent<Document>().Enumerator);
            Destroy(playerDocuments[playerDocuments.Count - 1]);
            playerDocuments.Remove(playerDocuments[playerDocuments.Count - 1]);
        }
    }

    public IEnumerator Work()
    {       
            if (documents.Count != 0)
            {
                yield return new WaitForSeconds(1.5f);
                DoWork();
            }
        flagWork = false;
    }

}