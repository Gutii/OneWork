using System.Collections.Generic;
using UnityEngine;

public class Document:MonoBehaviour
{    
    public int Enumerator = 0;
    public int Money = 10;
    public int Reputation = 5;
    public int Work = 30;    

    //public Document(int Money, int re)

    public void DoWork(int efficiency)
    {
        if(Work > Enumerator)
        {
            Destroy(gameObject);
        }

    }

}