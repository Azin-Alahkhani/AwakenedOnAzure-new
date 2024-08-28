using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMachine : MonoBehaviour
{

    public GameObject startUpPanel;
    private Button startBuy;
    private Button startRiddle;

    private GameObject panel;
    private Button[] buyButtons;
    private const int price = 2;

    public GameObject riddlePanel;
    
    private Riddle currentRiddle;

    private TMP_Text riddleQuestionTxt;
    private TMP_Text[] riddleAnswerTxts;
    private Button[] riddleAnswers;

    // Define resource types for each button
    public ResourceType[] itemResourceTypes;
    bool done=false;
    private void Awake()
    {

       SetUp();
        
       

        
    }

 void SetUp(){
    if(ResourceManager.instance!= null ){
             startUpPanel = ResourceManager.instance.startUpPanel;
        startBuy =  ResourceManager.instance.startBuy;
        startRiddle = ResourceManager.instance.startRiddle;
        panel = ResourceManager.instance.panel;
        buyButtons = ResourceManager.instance.buyButtons;
        riddleQuestionTxt = ResourceManager.instance.riddleQuestionTxt;
        riddleAnswerTxts = ResourceManager.instance.riddleAnswerTxts;
        riddleAnswers = ResourceManager.instance.riddleAnswers;
        riddlePanel = ResourceManager.instance.riddlePanel;
        done = true;

        }
 }
    void Update(){
        if(!done){
            SetUp();
        }
    }

private void Start(){

        buyButtons[0].onClick.AddListener(() => OnBuy(0));
        buyButtons[1].onClick.AddListener(() => OnBuy(1));
        
        riddleAnswers[0].onClick.AddListener(() => { OnPlayerAnswer(0); });
        riddleAnswers[1].onClick.AddListener(() => { OnPlayerAnswer(1); });
        riddleAnswers[2].onClick.AddListener(() => { OnPlayerAnswer(2); });
        riddleAnswers[3].onClick.AddListener(() => { OnPlayerAnswer(3); });

        startBuy.onClick.AddListener(() => {
             Debug.Log("clicked to buy");
            riddlePanel.SetActive(false);
            panel.SetActive(true);
        });

        startRiddle.onClick.AddListener(() =>
        {
            Debug.Log("clicked to answer riddles");
            startUpPanel.SetActive(false);
        });
}
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            riddlePanel.SetActive(true);
            PickNewRiddle();
        }
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            panel.SetActive(false);
            riddlePanel.SetActive(false);
            startUpPanel.SetActive(true);
        }
    }
    
    private void PickNewRiddle()
    {
        currentRiddle = RiddleManager.instance.GetRandomRiddle();

        if (currentRiddle != null)
        {
            DisplayRiddle(currentRiddle);
        }
        else
        {
            Debug.Log("No more riddles available.");
            // Handle the case where no more riddlesFile are left
        }
    }
    private void DisplayRiddle(Riddle riddle)
    {
        if (riddle.TextQuestion != null)
        {
            // Display the text question and text answers in the UI
            riddleQuestionTxt.text = riddle.TextQuestion;
            for (int i = 0; i < 4; i++)
            {
                riddleAnswerTxts[i].text = riddle.TextAnswers[i];
            }
        }
        else if (riddle.ImageQuestion != null)
        {
            // Display the image question and image answers in the UI
        }
    }
    public void OnPlayerAnswer(int selectedAnswerIndex)
    {

        if(currentRiddle!= null){
            if (currentRiddle.IsCorrectAnswer(selectedAnswerIndex))
        {
            // Provide resource to the player
            Debug.Log("Correct Answer! Resource dispensed.");
            riddlePanel.SetActive(false);
            ResourceManager.instance.currencyLevels += 4;
            panel.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong Answer! Try again.");
            PickNewRiddle();
        }
        }

        
    }

    public void OnBuy(int index)
    {
        Debug.Log("Attempting to buy item " + index);
        ResourceType resourceType = itemResourceTypes[index];

        if (ResourceManager.instance.currencyLevels < price)
        {
            Debug.Log("Not enough money to buy item " + index);
            return;
        }
        bool canBuy = false;
        switch (index)
        {
            case 1:
               
                    canBuy = true;
                    ResourceManager.instance.SetAirLevels(ResourceManager.instance.resourceMax);
                
                break;
            case 0:
                
                    canBuy = true;
                    ResourceManager.instance.SetWaterLevels(ResourceManager.instance.resourceMax);
                
                break;
            default:
                Debug.LogWarning("Unsupported resource type");
                break;
        }

        if (canBuy)
        {
            ResourceManager.instance.ChangeCurrencyLevels(-price);
            Debug.Log("Bought item " + index);
        }
        

    }

    public enum ResourceType
    {
        Air,
        Water
    }
}
