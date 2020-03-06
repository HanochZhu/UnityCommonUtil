using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectToolEditorWindows : EditorWindow
{
    private static SelectToolEditorWindows _instance;

    private Transform selectedParent;

    private bool collapseSelectParentPanel = false;

    private bool collapseActiveAllPanel = false;

    private Rect normalButtonRect = new Rect(0, 0, 100, 50);

    [MenuItem("PRETools/SelectTool")]
    public static void InvokeWindow()
    {
        _instance = (SelectToolEditorWindows)EditorWindow.GetWindow(typeof(SelectToolEditorWindows), false);
        _instance.Show();
    }


    private void OnGUI()
    {
        DrawSelectFoldoutPanel();
        DrawActiveAllFoldoutPanel();
    }

    private void DrawSelectFoldoutPanel()
    {
        collapseSelectParentPanel = EditorGUILayout.Foldout(collapseSelectParentPanel, "Select Parent");
        // if menu don't collapse 
        if (collapseSelectParentPanel)
        {
            EditorGUILayout.BeginVertical("Set parent");

            selectedParent = (Transform)EditorGUILayout.ObjectField(selectedParent, typeof(Transform), true);

            if (selectedParent != null)
            {
                Transform[] children = Selection.transforms;

                if (children != null && children.Length != 0)
                {
                    if (children.Length == 1 && children[0] == selectedParent)
                    {
                        GUILayout.Label("Select objects in scene or hierarchy");
                        GUI.backgroundColor = Color.gray;
                        GUILayout.Button("Set selected as child");
                    }
                    else if (GUILayout.Button("Set selected as child"))
                    {
                        foreach (var item in children)
                        {
                            item.SetParent(selectedParent);
                        }
                        
                    }
                }
                else
                {
                    GUILayout.Label("Select objects in scene or hierarchy");
                    GUI.backgroundColor = Color.gray;
                    GUILayout.Button("Set selected as child");
                }
            }
            else
            {
                GUILayout.Label("Select object in scene or hierarchy as parent");
            }

            EditorGUILayout.EndVertical();
        }
    }

    private void DrawActiveAllFoldoutPanel()
    {
        collapseActiveAllPanel = EditorGUILayout.Foldout(collapseActiveAllPanel, "Select Parent");

        if (collapseActiveAllPanel)
        {
            EditorGUILayout.BeginVertical("Set children active");

            GUILayout.Label("Select a object which children need set active of disactive");

            Transform selected = Selection.activeTransform;// (Selection.transforms != null ? Selection.transforms[0] : null);
            if (selected != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Set all active, include select");
                if (GUILayout.Button("Set all active"))
                {
                    List<Transform> children = TransformUtils.GetAllChildrenIteration(selected);
                    if (children != null)
                    {
                        foreach (Transform item in children)
                        {
                            item.gameObject.SetActive(true);
                        }
                    }
                    if (selected.gameObject.activeSelf == false)
                    {
                        selected.gameObject.SetActive(true);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Set all disactive, include select");
                if (GUILayout.Button("Set all disactive"))
                {
                    List<Transform> children = TransformUtils.GetAllChildrenIteration(selected);
                    if (children != null)
                    {
                        foreach (Transform item in children)
                        {
                            item.gameObject.SetActive(false);
                        }
                    }
                    if (selected.gameObject.activeSelf == true)
                    {
                        selected.gameObject.SetActive(false);
                    }
                }
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndVertical();
        }
    }
}
