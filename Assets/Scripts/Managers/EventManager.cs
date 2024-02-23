using UnityEngine;
using System;
using System.Collections.Generic;

public class EventManager : BaseSingleton<EventManager>
{
    private Dictionary<string, Action<object[]>> eventDictionary;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<object[]>>();
        }
    }

    public static void StartListening(string eventName, Action<object[]> listener)
    {
        Action<object[]> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<object[]> listener)
    {
        Action<object[]> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static bool ArrayNullOrEmptyCheck(object[] obj)
    {
        if (obj == null || obj != null && obj.Length == 0)
        {
            Debug.LogError("Array empty or null. Please check your code!!");
            return true;
        }
        return false;
    }

    public static void TriggerEvent(string eventName, object[] parameters)
    {
        Action<object[]> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(parameters);
        }
    }
}
