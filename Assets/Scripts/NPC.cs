using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, Iinteractable
{
    public GameObject[] vClue;
    private bool isWithinRange = false;

    public TextAsset inkJSON;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger area");
            vClue[0].SetActive(true);
            vClue[1].SetActive(true);
            isWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vClue[0].SetActive(false);
            vClue[1].SetActive(false);
            isWithinRange = false;
        }
    }
    public void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame && isWithinRange)
        {
            Interact(inkJSON);
            vClue[0].SetActive(false);
            vClue[1].SetActive(false);

        }
    }

    public abstract void Interact(TextAsset inkJSON);
}
