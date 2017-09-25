using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class DialogueTreeEditor : EditorWindow
{

    List<DialogueTree> treeList;
    DialogueTree tempTree;
    Vector2 scrollList;
    SpecificDialogueEditor specDiagEditor;

    Dialogue curDialogue;
    List<DialogueNode> diagNodes;
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();

    float panX = 0;
    float panY = 0;



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
        diagNodes = new List<DialogueNode>();
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
                diagNodes = new List<DialogueNode>();
                windowsToAttach = new List<int>();
                attachedWindows = new List<int>();

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

    void DrawNodeWindow(int id)
    {
        if (GUILayout.Button("Attach"))
        {
            windowsToAttach.Add(id);
        }
        diagNodes[id].Diag.Speech = EditorGUILayout.TextField("Speech:", diagNodes[id].Diag.Speech);

        GUI.DragWindow();
    }


    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < 3; i++)
        {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    private void displayTreeInfo()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.BeginHorizontal();

        tempTree.SceneID = EditorGUILayout.IntField("ID given to scene by unity build manager: ", tempTree.SceneID);
        tempTree.SceneName = EditorGUILayout.TextField("Name given to the scene:", tempTree.SceneName);
        GUILayout.BeginHorizontal();
        dialogueOptions();

        EditorGUILayout.EndVertical();
    }

    private void dialogueOptions()
    {
        //create a node 
        if (GUILayout.Button("Create Dialgoue"))
        {
            diagNodes.Add(new DialogueNode(new Rect(Mathf.Abs(panX) + 50, Mathf.Abs(panY) + 50, 200, 200), new Dialogue()));
        }

        if (Event.current.rawType == EventType.mouseDrag && Event.current.button == 1)
        {
            panX += Event.current.delta.x;
            panY += Event.current.delta.y;
            Repaint();
        }

        GUI.BeginGroup(new Rect(panX, panY, 10000, 10000));



        GUILayout.BeginHorizontal();
        if (windowsToAttach.Count == 2)
        {
            attachedWindows.Add(windowsToAttach[0]);
            attachedWindows.Add(windowsToAttach[1]);
            windowsToAttach = new List<int>();
        }

        if (attachedWindows.Count >= 2)
        {
            for (int i = 0; i < attachedWindows.Count; i += 2)
            {
                DrawNodeCurve(diagNodes[attachedWindows[i]].Rect, diagNodes[attachedWindows[i + 1]].Rect);
            }
        }

        BeginWindows();

        for (int i = 0; i < diagNodes.Count; i++)
        {
            diagNodes[i].Rect = GUI.Window(i, diagNodes[i].Rect, DrawNodeWindow, "Window " + i);
        }

        EndWindows();

        GUI.EndGroup();


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
                    tempTree.Dialogues = t.Dialogues;
                    editTree = true;
                    foreach (Dialogue d in tempTree.Dialogues)
                    {
                        diagNodes.Add(new DialogueNode(new Rect(panX, panY, 200, 200), d));
                    }
                }
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.Label("#" + treeList.Count + " dialogue trees");
        EditorGUILayout.EndVertical();
    }
}
