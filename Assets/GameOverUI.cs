using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameOverUI : MonoBehaviour
{

    public Button restart;
    public Button exit;

    public ResultType type;

    private void Start()
    {
        restart.onClick.AddListener(Restart);
        exit.onClick.AddListener(ExitGame);
    }

    public void Restart()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (type == ResultType.Succeed)
        {
            SceneManager.LoadSceneAsync(sceneName == "Gaming" ? "GamingEvening" : "Gaming", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync("Start", LoadSceneMode.Single);
        }
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
#if UNITY_EDITOR
    [MenuItem("工具/截图 %F9")]
    public static void Capture()
    {
        ScreenCapture.CaptureScreenshot($"screen/{System.DateTime.Now.Millisecond}.jpg"
        , 1);
    }
#endif
}
