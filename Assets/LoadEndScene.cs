using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndScene : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; 
    public float fadeDuration = 2f; 
    public int nextSceneIndex = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndLoadScene(nextSceneIndex));
        }
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        // Start fading to black
        yield return StartCoroutine(FadeToBlack());

        // Load the next scene
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator FadeToBlack()
    {
        float timer = 0f;

        // Gradually increase the alpha value over time to fade out
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        // Ensure the screen is fully black
        fadeCanvasGroup.alpha = 1f;
    }
}
