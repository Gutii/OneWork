using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Document
{
    public int Sprite { get; set; }
    public int Enumerator { get; set; } = 0;
    public int _money;
    public int _reputation;
    public int work;

    public Document(int money,int reputation)
    {
        _money = money;
        _reputation = reputation;
        work = 5;
    }

    public void GetDocument(Document document)
    {
        Sprite = document.Sprite;
        Enumerator = document.Enumerator;
        _money = document._money;
        _reputation = document._reputation;
        work = document.work;
    }


}

[System.Serializable]
public class DocumentRange
{    
    public int MoneyMin;
    public int MoneyMax;
    public int ReputationMin;
    public int ReputationMax;
}