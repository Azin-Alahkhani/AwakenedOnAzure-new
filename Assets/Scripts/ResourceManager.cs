using UnityEngine;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;


public GameObject startUpPanel;
    public Button startBuy;
    public Button startRiddle;

    public GameObject panel;
    public Button[] buyButtons;

    public GameObject riddlePanel;
   

    public TMP_Text riddleQuestionTxt;
    public TMP_Text[] riddleAnswerTxts;
    public Button[] riddleAnswers;

    public float airLevels = 100;
    public float waterLevels = 100;
    public int currencyLevels = 0;
    public int resourceMax = 100;

    public float airDecreaseRate = 0.35f;
    public float waterDecreaseRate = 0.35f;

    public Action<bool> isOutOfResource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(DecreaseResourcesOverTime());
    }

    private IEnumerator DecreaseResourcesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);  
            ChangeAirLevels(-airDecreaseRate); 
            ChangeWaterLevels(-waterDecreaseRate);
        }
    }

    public void ChangeAirLevels(float amount)
    {
        airLevels = Mathf.Clamp(airLevels + amount, 0, resourceMax);  
        //if (airLevels <= 0 && isOutOfResource != null)
           // isOutOfResource.Invoke(true);
    }

    public void ChangeWaterLevels(float amount)
    {
        waterLevels = Mathf.Clamp(waterLevels + amount, 0, resourceMax);  
        //if (waterLevels <= 0 && isOutOfResource != null)
            //isOutOfResource.Invoke(true);
    }

    public void ChangeCurrencyLevels(int amount)
    {
        currencyLevels += amount;
    }

    public void SetAirLevels(float amount)
    {
        airLevels = amount;
        Debug.Log("Air levels set to: " + airLevels);
    }

    public void SetWaterLevels(float amount)
    {
        waterLevels = amount;
        Debug.Log("Water levels set to: " + waterLevels);
    }
}
