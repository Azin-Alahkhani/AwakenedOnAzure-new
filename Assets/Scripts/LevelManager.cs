using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Action<bool> OpenDoorSignal;

    public static int reportResult = 0; // 0 is for keeping, 1 is for destroting. toggle as needed
    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
    }
    private void Start()
    {
        // Subscribe to the event in ResourceManager
        ResourceManager.instance.isOutOfResource += EndTheGame;
    }

    public void EndTheGame(bool over)
    {
        Debug.Log("Your resources ended. You died. Game over.");
        //SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the LevelManager is destroyed
        if (ResourceManager.instance != null)
        {
            ResourceManager.instance.isOutOfResource -= EndTheGame;
        }
    }
    public void OpenTheDoor()
    {
        OpenDoorSignal.Invoke(true);
    }
    public void CloseTheDoor()
    {
        OpenDoorSignal.Invoke(false);
    }
}
