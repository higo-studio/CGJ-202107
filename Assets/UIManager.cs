using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIManager : MonoBehaviour 
{
    public Button btn1;
   //...
 
    void Awake()
    {
        btn1 = this.transform.Find("Button").GetComponent<Button>();
        AddTriggersListener(btn1.gameObject, EventTriggerType.PointerClick, StartGame);
    }
 
   
    void StartGame(BaseEventData data)
    {
        Debug.Log("start game");
    }
 
    private void AddTriggersListener(GameObject obj, EventTriggerType eventID, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }
 
        if (trigger.triggers.Count == 0)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }
 
        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
}}
