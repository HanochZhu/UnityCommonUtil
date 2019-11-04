/**
 * UI 辅助类
 * 提供UI相关的查找、等等功能的函数
 * 与具体UI无关
 * 通过string来查找
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentHelper
{
    private Transform transform;
    public Transform ComponentTransform
    {
        get
        {
            return transform;
        }
        private set
        {
            transform = value;
        }
    }
    // index使用方法
    // Transform t = UIComponentHelper[parent1][parent2]..[curent].ComponentTransform
    public UIComponentHelper this[string name]
    {
        get
        {
            return null;
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public void SetRootAnchor(Transform transform)
    {
        this.ComponentTransform = transform;
    }
}
