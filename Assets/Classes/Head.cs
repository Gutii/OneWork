using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    private Company company;
    private Player player;

    [SerializeField] private Text CountWorker;
    [SerializeField] private Text MoneyHead;
    [SerializeField] private Text LevelReputation;

    [SerializeField] private Slider SliderReputation;
    [SerializeField] private Text ChengSliderRep;

    [SerializeField] private Slider CompletWork;
    [SerializeField] private Text ChengSliderWork;


    private void Start()
    {
        company = GameObject.FindObjectOfType<Company>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        try
        {
        company.RepLevelUP();
        MoneyHead.text = company.Money.ToString();
        CountWorker.text = company.CountWorker.ToString();
        SliderUpdate(SliderReputation, ChengSliderRep, company.Reputation, company.ReputationMax);
            if(player!=null)
        if (player.documents != null)
            if (player.documents.Count != 0)
                SliderUpdate(CompletWork, ChengSliderWork, player.documents[player.documents.Count - 1].GetComponent<Document>().Enumerator, player.documents[player.documents.Count - 1].GetComponent<Document>().Work);
            else
                SliderUpdate(CompletWork, ChengSliderWork, 0, 0);
        else
            SliderUpdate(CompletWork, ChengSliderWork, 0, 0);

        }
        catch(System.Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void SliderUpdate(Slider slider, Text text, int cheng, int max)
    {
        text.text = cheng.ToString() + "/" + max.ToString();
        slider.maxValue = max;
        slider.value = cheng;
    }
}