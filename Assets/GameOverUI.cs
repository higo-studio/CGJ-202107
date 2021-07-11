using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{

    public Button restart;
    public Button exit;


    private void Start()
    {
        restart.onClick.AddListener(Restart);
        exit.onClick.AddListener(ExitGame);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Start", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
