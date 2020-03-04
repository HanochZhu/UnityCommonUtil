/**
 * c# extension scipts
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// all of this extension method are static type
// so as this scipts
public static class CoreExtension
{
    public static void Register(this IEventProxy self,EventKeys key,DisptchObject obj)
    {
        EventListener.Instance.RegistEvent(key, obj);
    }

    public static void Register(this IEventProxy self, EventKeys key,UnityAction<object> obj)
    {
        DisptchObject disptchObject = new DisptchObject();
        disptchObject.Init(obj);
        EventListener.Instance.RegistEvent(key, disptchObject);
    }

    public static void Register(this IEventProxy self, EventKeys key, UnityAction obj)
    {
        DisptchObject disptchObject = new DisptchObject();
        disptchObject.Init(obj);
        EventListener.Instance.RegistEvent(key, disptchObject);
    }

    public static void Invoke(this IEventProxy self, EventKeys key, object parameter)
    {
        EventListener.Instance.InvokeEvent(key, parameter);
    }

    public static void Invoke(this IEventProxy self, EventKeys key)
    {
        EventListener.Instance.InvokeEvent(key);
    }
}
