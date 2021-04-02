using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    public override Company Company { get; set; }

    public override bool Fatigue { get; set; }
    public override List<GameObject> Documents { get; set; } = new List<GameObject>();
    public override GameObject Table { get; set; }
    public override Data data { get; set; } = new Data();

    private void Start()
    {
        Company = GameObject.Find("Canvas").GetComponent<Company>();
    }
  
    
}