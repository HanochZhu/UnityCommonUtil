/**
 * c# extension scipts
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all of this extension method are static type
// so as this scipts
public static class CoreExtension
{
    public static void Register(this IEventProxy self,EventKeys key,DisptchObject obj)
    {
        EventListener.instance.RegistEvent(key, obj);
    }

    public static void Invoke(this IEventProxy self, EventKeys key, object parameter)
    {
        EventListener.instance.InvokeEvent(key, parameter);
    }

    public static void Invoke(this IEventProxy self, EventKeys key)
    {
        EventListener.instance.InvokeEvent(key);
    }
}
