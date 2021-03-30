using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    private Company company;
    [HideInInspector] public Player player;

    [SerializeField] private Text countWorker;
    [SerializeField] private Text MoneyHead;
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
        MoneyHead.text = company.companyData.Money.ToString();
        countWorker.text = company.companyData.CountWorker.ToString();
        SliderUpdate(sliderReputation, chengSliderRep, company.companyData.Reputation, company.ReputationMax);
            if(player!=null)
            if (player.data.documents.Count != 0)
                SliderUpdate(completWork, chengSliderWork, player.data.documents[player.Documents.Count - 1].Enumerator, player.data.documents[player.Documents.Count - 1].work);
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