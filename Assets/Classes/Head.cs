using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    private Company company;
    [HideInInspector] public Player player;

    [SerializeField] private Text countWorker;
    [SerializeField] private Text moneyHead;
    [SerializeField] private Text levelReputation;

    [SerializeField] private Slider sliderReputation;
    [SerializeField] private Text chengSliderRep;

    [SerializeField] private Slider completWork;
    [SerializeField] private Text chengSliderWork;


    private void Start()
    {
        company = GameObject.FindObjectOfType<Company>();
    }

    private void FixedUpdate()
    {
        try
        {
        company.RepLevelUP();
        moneyHead.text = company.companyData.money.ToString();
        countWorker.text = company.companyData.CountWorker.ToString();
        SliderUpdate(sliderReputation, chengSliderRep, company.companyData.reputation, company.ReputationMax);
            if(player!=null)
        if (player.Documents.Count != 0)
            if (player.Documents.Count != 0)
                SliderUpdate(completWork, chengSliderWork, player.Documents[player.Documents.Count - 1].GetComponent<Document>().Enumerator, player.Documents[player.Documents.Count - 1].GetComponent<Document>().Work);
            else
                SliderUpdate(completWork, chengSliderWork, 0, 0);
        else
            SliderUpdate(completWork, chengSliderWork, 0, 0);

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