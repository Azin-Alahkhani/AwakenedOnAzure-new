using System.Collections;
using UnityEngine;
using TMPro; 

public class ShowReport : MonoBehaviour
{

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 2f;

    public TextMeshProUGUI reportTitleText; 
    public TextMeshProUGUI reportSummaryText;
    public float typingSpeed = 0.1f; 
    

    private string title1 = "Critical Condition: Immediate Action Required";
    private string summary1 = "The planet is in a dire state, with ecological systems on the verge of collapse and no feasible recovery in sight. Continued existence poses a significant threat to nearby celestial bodies. It is recommended that immediate obliteration be considered to prevent further degradation and potential contamination of the surrounding space.";

    private string title2 = "Endangered But Recoverable: Preservation Recommended";
    private string summary2 = "While the planet's current condition is severe, with significant environmental degradation, there are signs of potential recovery. With intervention and time, the planet may stabilize and even thrive. It is advised that destruction be withheld and the planet be allowed the opportunity to rehabilitate naturally.";


    public AudioClip beepSound; // The beep sound effect
    public AudioSource audioSource;
    public float minPitch = 0.8f; // Minimum pitch value
    public float maxPitch = 1.2f; // Maximum pitch value

    void Start()
    {
        StartCoroutine(FadeIn());
        
    }

    IEnumerator TypeText(string fullText)
    {
        foreach (char letter in fullText.ToCharArray())
        {
            reportSummaryText.text += letter;

            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();

            yield return new WaitForSeconds(typingSpeed); 
        }
    }
    private IEnumerator FadeIn()
    {
        float timer = 0f;

        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        
        fadeCanvasGroup.alpha = 0f;

        //int i = LevelManager.reportResult;
        int i = 1;

        if (i == 1)
        {
            reportTitleText.text = title1;
            reportSummaryText.text = "";
            StartCoroutine(TypeText(summary1));
        }
        else if (i == 0)
        {
            reportTitleText.text = title2;
            reportSummaryText.text = ""; // Clear text initially
            StartCoroutine(TypeText(summary2));
        }
    }
}
