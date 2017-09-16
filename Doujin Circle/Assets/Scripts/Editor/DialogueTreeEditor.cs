using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class DialogueTreeEditor : EditorWindow {

    List<DialogueTree> treeList;
    DialogueTree tempTree;
    Vector2 scrollList;
    SpecificDialogueEditor specDiagEditor;

    bool editTree;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Editors/Dialogue Trees")]
    static void Init()
    {

        // Get existing open window or if none, make a new one:
        DialogueTreeEditor window = (DialogueTreeEditor)EditorWindow.GetWindow(typeof(DialogueTreeEditor));
        window.titleContent.text = "Dialogue Tree Editor";
        window.Show();

    }

    void OnEnable()
    {
        loadAllTrees();
    }

    private void loadAllTrees()
    {
        treeList = new List<DialogueTree>();
        string[] files = Directory.GetFiles(@"A:\Doujin Circle\Git\Doujin Circle\Assets\json\trees", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(files[i]);
            treeList.Add(JsonHelper<DialogueTree>.CreateFromJSON(json));
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        listTrees();
        if (editTree)
        {
            displayTreeInfo();
        }
        else
        {
            if (GUILayout.Button("New Dialogue Tree"))
            {
                tempTree = new DialogueTree();
                editTree = true;
            }
            if (tempTree != null)
            {
                if (GUILayout.Button("Restore"))
                {
                    editTree = true;
                }
            }
        }
        GUILayout.EndHorizontal();

    }

    private void displayTreeInfo()
    {
        EditorGUILayout.BeginVertical();
        tempTree.SceneID = EditorGUILayout.IntField("ID given to scene by unity build manager: ", tempTree.SceneID);
        tempTree.SceneName = EditorGUILayout.TextField("Name given to the scene:", tempTree.SceneName);
        dialogueOptions();
        EditorGUILayout.EndVertical();
    }

    private void dialogueOptions()
    {
        GUILayout.BeginHorizontal();

        if (tempTree.StartDialogue == null)
        {
            if (GUILayout.Button("Create New Dialogue"))
            {
                tempTree.StartDialogue = new Dialogue();
                //after thats done start a new dialogue editor window for specific dialogue editing
                // Get existing open window or if none, make a new one:
             

            }
        }
        else
        {
            //only show creation option if starting dialogue has not been set yet
        }
 

        GUILayout.EndHorizontal();
    }

    private void listTrees()
    {
        EditorGUILayout.BeginVertical();
        scrollList = EditorGUILayout.BeginScrollView(scrollList);
        if (treeList.Count <= 0)
        {
            GUILayout.Label("No Dialogue Trees make one");
        }
        else
        {
            //Add some other way such as showing sprites to identify the characters
            foreach (DialogueTree t in treeList)
            {

                if (GUILayout.Button(t.SceneName))
                {
                    tempTree = t;
                    editTree = true;
                }
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.Label("#" + treeList.Count + " dialogue trees");
        EditorGUILayout.EndVertical();
    }
}
