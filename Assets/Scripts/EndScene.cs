using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public Button restartBtn;
    public Button exitBtn;
    void Awake()
    {
        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        exitBtn.onClick.AddListener(()=> Application.Quit());
    }

}
