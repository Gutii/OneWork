using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Worker : Human
{
    public override Company Company { get; set; }

    public override List<GameObject> Documents { get; set; } = new List<GameObject>();
    public override bool Fatigue { get; set; }
    public override GameObject Table { get; set; }
    public override Data data { get; set; } = new Data();
    private bool flagWork;
   

    private void Start()
    {
        Company = GameObject.Find("Canvas").GetComponent<Company>();
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
        Player player = Company.workers[0].transform.Find("Worker").GetComponent<Player>();
        if (Documents.Count <= 10 && player.Documents.Count != 0)
        {
            Document playerDocument = player.data.documents[player.data.documents.Count-1];        
            AcceptedJob(playerDocument);
            Destroy(player.Documents[player.Documents.Count-1]);
            player.data.documents.Remove(player.data.documents[player.data.documents.Count - 1]);
            player.Documents.Remove(player.Documents[player.Documents.Count - 1]);
        }
    }

    public IEnumerator Work()
    {       
            if (Documents.Count != 0)
            {
                yield return new WaitForSeconds(1.5f);
                DoWork();
            }
        flagWork = false;
    }

}