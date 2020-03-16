/**
 * mini event manager
 * regist event
 * disptch event
 * /@ hongqiang.zhu todo
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener 
{
    private static EventListener _instance;

    public static EventListener Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventListener();
            }
            return _instance;
        }
    }

    private Dictionary<EventKeys,List< DisptchObject>> eventDictionary;

    private EventListener()
    {
        eventDictionary = new Dictionary<EventKeys, List<DisptchObject>>();
    }

    public void RegistEvent(EventKeys key,DisptchObject obj)
    {
        if (!eventDictionary.ContainsKey(key))
        {
            eventDictionary.Add(key, new List<DisptchObject>());
        }
        eventDictionary[key].Add(obj);
    }

    public void InvokeEvent(EventKeys key,object parameters)
    {
        if (eventDictionary.ContainsKey(key))
        {
            List<DisptchObject> objects = eventDictionary[key];

            foreach (var item in objects)
            {
                item.Invoke(parameters);
            }
        }
    }

    public void InvokeEvent(EventKeys key)
    {
        if (eventDictionary.ContainsKey(key))
        {
            List<DisptchObject> objects = eventDictionary[key];

            foreach (var item in objects)
            {
                item.Invoke();
            }
        }
    }
}

public class DisptchObject
{
    public EventKeys key;

    private UnityAction<object> target; 
    private UnityAction targetNoParameter;

    public void Init(UnityAction<object> callback)
    {
        target = callback;
    }

    public void Init(UnityAction callback)
    {
        targetNoParameter = callback;
    }

    public void Invoke(object parameters)
    {
        target?.Invoke(parameters);
    }

    public void Invoke()
    {
        targetNoParameter?.Invoke();
    }
}