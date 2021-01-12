using System.Collections.Generic;
using UnityEngine;

public class Worker : Human
{
    public override Company company { get; set; }
    public override int efficiency { get; set; } = 1;

    //  public override int fatigue { get; set; }
    public override List<GameObject> documents { get; set; } = new List<GameObject>();
    public override bool fatigue { get; set; }
    public override GameObject Table { get; set; }

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }
}