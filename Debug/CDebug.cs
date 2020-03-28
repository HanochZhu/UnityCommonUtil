/**
 * Time:3.28.2020
 * Task: Debug的封装
 * 用于处理不同情况下debug的不同方式
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDebug 
{
    // 暂时先用unity自带的，后面再补充
    public static void CDebugErrorLog(object o)
    {
        Debug.LogError(o.ToString());
    }
}
