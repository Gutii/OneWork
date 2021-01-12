using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    public override Company company { get; set; }
    public override int efficiency { get; set; } = 1;

    public override bool fatigue { get; set; }
    public override List<GameObject> documents { get; set; } = new List<GameObject>();
    public override GameObject Table { get { return tableInspector; } set { } }

    public GameObject tableInspector;

    private void Start()
    {
        company = GameObject.Find("Canvas").GetComponent<Company>();
    }
  
}