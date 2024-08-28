using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
   public GameObject door;
    public GameObject sudokuPanel;
    void Start()
    {
        LevelManager.instance.OpenDoorSignal += UpdateDoor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) ShowSudoku();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) HideSudoku();
    }

    // Update is called once per frame
    void UpdateDoor(bool o)
    {
        if (o)
        {
            door.SetActive(false);
            HideSudoku();
        }
        else door.SetActive(true);
    }
    void ShowSudoku() { sudokuPanel.SetActive(true); }
    void HideSudoku() { sudokuPanel.SetActive(false); }


}
