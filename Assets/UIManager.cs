using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;

    void Awake()
    {
        // AddTriggersListener(startBtn.gameObject, EventTriggerType.PointerClick, StartGame);
        startBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync("Gaming", LoadSceneMode.Single);
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();        
#else
        Application.Quit();
#endif
    }

    // private void AddTriggersListener(GameObject obj, EventTriggerType eventID, UnityAction<BaseEventData> action)
    // {
    //     EventTrigger trigger = obj.GetComponent<EventTrigger>();
    //     if (trigger == null)
    //     {
    //         trigger = obj.AddComponent<EventTrigger>();
    //     }

    //     if (trigger.triggers.Count == 0)
    //     {
    //         trigger.triggers = new List<EventTrigger.Entry>();
    //     }

    //     UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
    //     EventTrigger.Entry entry = new EventTrigger.Entry();
    //     entry.eventID = eventID;
    //     entry.callback.AddListener(callback);
    //     trigger.triggers.Add(entry);
    // }
}
