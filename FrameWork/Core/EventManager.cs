using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager 
{
    public delegate void OnFreeEventCall<T>(T t);
    public delegate void OnFreeEventCall<T,U>(T t,U u);
    public delegate void OnFreeEventCall<T,U,V>(T t,U u,V v);
    public delegate void OnFreeEventCall(params object[] objs);

    public static Dictionary<FreeEventKey, OnFreeEventCall<object>> OnFreeEventCallFrequency = new Dictionary<FreeEventKey, OnFreeEventCall<object>>();

    public static Dictionary<FreeEventKey, OnFreeEventCall> OnFreeEventCallFrequencyParams = new Dictionary<FreeEventKey, OnFreeEventCall>();

    public static Dictionary<FreeEventKey, OnFreeEventCall<object>> OnFreeEventCallOnce = new Dictionary<FreeEventKey, OnFreeEventCall<object>>();
    public static Dictionary<FreeEventKey, OnFreeEventCall> OnFreeEventCallOnceParams = new Dictionary<FreeEventKey, OnFreeEventCall>();

    public static void AddAEventFrequencyLisenser(FreeEventKey _k,OnFreeEventCall<object> _e)
    {
        if (OnFreeEventCallFrequency.ContainsKey(_k))
        {
            OnFreeEventCallFrequency[_k] += _e;
        }
    }

    public static void SendAEvent(FreeEventKey _k,object _p)
    {
        if (!CallAEventFrequency(_k,_p))
        {
            if(!CallAEventOnce(_k,_p))
            {
                // 没有函数响应，需要抛出异常
                Debug.LogError("Can not find the event key :" + _k);
                //throw new System.Exception("Can not find the event key :" + _k);
            }
        }
    }

    public static bool CallAEventFrequency(FreeEventKey _k,object _p)
    {
        if (OnFreeEventCallFrequency.ContainsKey(_k))
        {
            OnFreeEventCallFrequency[_k].Invoke(_p);
            return true;
        }
        return false;
    }

    public static bool CallAEventOnce(FreeEventKey _k, object _p)
    {
        if (OnFreeEventCallOnce.ContainsKey(_k))
        {
            OnFreeEventCallOnce[_k].Invoke(_p);
            OnFreeEventCallOnce.Remove(_k);
            return true;
        }
        return false;
    }
}
