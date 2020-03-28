using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils 
{
    public static List<Transform> GetAllChildrenIteration(Transform parent)
    {
        // in fact, you can use getcomponentsinchildren<tranform>() 
        // to get every child under parent, even in recrusive situation
        if (parent.childCount < 1)
        {
            return null;
        }

        List<Transform> children = new List<Transform>();

        foreach (Transform item in parent)
        {
            children.Add(item);
            List<Transform> tc = GetAllChildrenIteration(item);
            if (tc != null)
            {
                children.AddRange(tc);
            }
        }
        return children;
    }

    public static Transform[] GetAllChildrenUnderTransform(Transform tr)
    {
        return tr.GetComponentsInChildren<Transform>();
    }
}
