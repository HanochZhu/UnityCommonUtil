/**
 * 搜索组件丢失的对象
 * 并且将空对象销毁
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SearchMissingComponent : EditorWindow
{
    [MenuItem("Window / ClearMissingScripts")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SearchMissingComponent));
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0.0f, 60.0f, 130.0f, 20.0f), "Clear Select Object"))
        {
            GameObject[] goes = Selection.gameObjects;
            foreach (GameObject go in goes)
            {
                Component[] components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        DestroyImmediate(components[i]);
                    }
                }
            }
        }
        if (GUI.Button(new Rect(0.0f, 80.0f, 130.0f, 20.0f), "Clear Hierarchy Objects"))
        {
            GameObject[] goes = FindObjectsOfType<GameObject>();
            foreach (GameObject go in goes)
            {
                Component[] components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        DestroyImmediate(components[i]);
                    }
                }
            }
        }
        if (GUI.Button(new Rect(0.0f, 100.0f, 130.0f, 20.0f), "Clear All Inspector Objects"))
        {
            string[] gids= AssetDatabase.FindAssets("t:Prefab");
            foreach (var gid in gids)
            {
                string path = AssetDatabase.GUIDToAssetPath(gid);
                Debug.Log(path);
                GameObject go = PrefabUtility.LoadPrefabContents(path);
                //GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (go)
                {
                    Component[] components = go.GetComponents<Component>();
                    for (int i = 0; i < components.Length; i++)
                    {
                        if (components[i] == null)
                        {
                            PrefabUtility.ApplyRemovedComponent(go, components[i], InteractionMode.AutomatedAction);
                        }
                    }
                }
            }
            
        }

    }
}
