using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DialogueEditor : EditorWindow
{

    List<NodeTree> trees;
    NodeTree tempTree;
    List<EditorNode> editorNodes;
    Vector2 scrollList;
    bool editingTree;

    List<Character> characterList;
    List<Vector2> scrolls;

    string[] Option;

    //Used to visually display nodes attaching
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();

    float panX, panY;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Editors/Dialogue Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DialogueEditor window = (DialogueEditor)EditorWindow.GetWindow(typeof(DialogueEditor));
        window.Show();
    }

    void OnEnable()
    {
        loadTrees();
        loadCharacters();
    }

    private void loadTrees()
    {
        trees = new List<NodeTree>();
        string[] files = Directory.GetFiles(@"A:\Doujin Circle\Git\Doujin Circle\Assets\json\trees", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(files[i]);
            trees.Add(JsonHelper<NodeTree>.CreateFromJSON(json));
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        displayList();
        editorSpace();
        GUILayout.EndHorizontal();
    }

    private void editorSpace()
    {
        GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("This is the Tree editor space.");
        if (editingTree)
        {
            TreeInfo();
            ButtonOptions();
            NodeArea();
        }
        else
        {
            if (GUILayout.Button(" Make a new Tree. "))
            {
                tempTree = new NodeTree();
                editorNodes = new List<EditorNode>();
                scrolls = new List<Vector2>();
                editingTree = true;
            }
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// Handles all the window rendering logic
    /// </summary>
    private void NodeArea()
    {
        GUI.BeginGroup(new Rect(panX, panY, 100000, 100000));
        if (windowsToAttach.Count == 2)
        {
            attachedWindows.Add(windowsToAttach[0]);
            attachedWindows.Add(windowsToAttach[1]);

            editorNodes[windowsToAttach[0]].dialogue.Choices.Add(windowsToAttach[1]);
            windowsToAttach = new List<int>();
        }

        if (attachedWindows.Count >= 2)
        {
            for (int i = 0; i < attachedWindows.Count; i += 2)
            {
                DrawNodeCurve(editorNodes[attachedWindows[i]].rect, editorNodes[attachedWindows[i + 1]].rect);
                //Store the conection in choices list
            }
        }

        BeginWindows();


        for (int i = 0; i < editorNodes.Count; i++)
        {
            editorNodes[i].rect = GUI.Window(i, editorNodes[i].rect, DrawNodeWindow, "Window " + i);
        }

        EndWindows();
        GUI.EndGroup();
    }

    /// <summary>
    /// This is used to update each node within the editor view. Everything within here directly effects the dialogue values;
    /// </summary>
    /// <param name="id"></param>
    void DrawNodeWindow(int id)
    {

        scrolls[id] = GUILayout.BeginScrollView(scrolls[id]);
        Dialogue d = editorNodes[id].dialogue;

        d.HasChoices = EditorGUILayout.Toggle("Choice Available : ", d.HasChoices);
        if (d.HasChoices)
        {
            if (GUILayout.Button("Attach"))
            {
                windowsToAttach.Add(id);
            }
            foreach (int i in d.Choices)
            {
                EditorGUILayout.LabelField("Connected to node #" + i);
                if (GUILayout.Button("X"))
                {
                    d.Choices.Remove(i);
                    editorNodes[i].recieved = false;
                }
            }
        }
        else
        {
            if (GUILayout.Button("End Node"))
            {

                editorNodes[id].recieved = true;
                windowsToAttach.Add(id);
                d.Choices = new List<int>();
            }
        }
        d.Character = EditorGUILayout.Popup("Character ID : ", d.Character, Option);
        string[] poses = new string[characterList[d.Character].Poses.Count];
        for (int i = 0; i < characterList[d.Character].Poses.Count; i++)
        {
            poses[i] = characterList[d.Character].Poses[i].name;
        }
        d.DisplayedPoseID = EditorGUILayout.Popup("Sprite Options : ", d.DisplayedPoseID, poses);
        d.SFX = (AudioClip)EditorGUILayout.ObjectField("SFX : ", d.SFX, typeof(AudioClip), false);
        d.Action = (DialogueActions)EditorGUILayout.EnumMaskField("Action : ", d.Action);

        if (GUILayout.Button("Add Language."))
        {
            d.Speech.Add(new GameText());
        }
        foreach (GameText t in d.Speech)
        {
            t.LanguageName = EditorGUILayout.TextField("Language Name :", t.LanguageName);
            t.Info = EditorGUILayout.TextField("Text :", t.Info);
            t.VoiceOver = (AudioClip)EditorGUILayout.ObjectField("Voice over for " + t.LanguageName, t.VoiceOver, typeof(AudioClip), false);

        }

        GUILayout.EndScrollView();
        GUI.DragWindow();
    }

    /// <summary>
    /// Used to draw connections between dialogues
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
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

    private void TreeInfo()
    {
        tempTree.TreeName = EditorGUILayout.TextField("Tree Name : ", tempTree.TreeName);
        tempTree.SceneID = EditorGUILayout.IntField("Scene to load : ", tempTree.SceneID);
        GUILayout.Label("Dialogue Count - " + editorNodes.Count);
    }

    private void ButtonOptions()
    {
        if (GUILayout.Button("Add dialogue"))
        {
            editorNodes.Add(new EditorNode(new Rect(Mathf.Abs(panX) + 50, Mathf.Abs(panY) + 50, 300, 400), new Dialogue()));
            scrolls.Add(new Vector2());
        }
        if (GUILayout.Button("Save"))
        {
            if (tempTree.TreeName != "")
            {
                foreach (EditorNode n in editorNodes)
                {
                    Debug.Log(n.dialogue.Speech);
                    tempTree.Dialogues.Add(n.dialogue);
                }
                JsonHelper<NodeTree>.SaveToJson(tempTree, @"A:\Doujin Circle\Git\Doujin Circle\Assets\json\trees\" + tempTree.TreeName + ".json");
                editingTree = false;
                loadTrees();
            }
            else
            {
                Debug.Log("Give the tree a name to save it");
            }
        }
        if (GUILayout.Button("Save and end"))
        {
            if (tempTree.TreeName != "")
            {
                foreach (EditorNode n in editorNodes)
                {
                    tempTree.Dialogues.Add(n.dialogue);
                }
                JsonHelper<NodeTree>.SaveToJson(tempTree, @"A:\Doujin Circle\Git\Doujin Circle\Assets\json\trees\" + tempTree.TreeName + ".json");
                editingTree = false;
                tempTree = new NodeTree();
                editorNodes = new List<EditorNode>();
                loadTrees();
            }
            else
            {
                Debug.Log("Give the tree a name to save it");
            }
        }
    }

    private void displayList()
    {
        EditorGUILayout.BeginVertical();
        scrollList = EditorGUILayout.BeginScrollView(scrollList, GUILayout.Width(100f));
        if (trees.Count <= 0)
        {
            GUILayout.Label("No Trees make one");
        }
        else
        {
            //Add some other way such as showing sprites to identify the characters
            foreach (NodeTree t in trees)
            {
                if (GUILayout.Button(t.TreeName))
                {
                    tempTree = t;
                    editorNodes = new List<EditorNode>();
                    foreach (Dialogue d in t.Dialogues)
                    {
                        editorNodes.Add(new EditorNode(new Rect(100, 100, 300, 400), d));
                        scrolls.Add(new Vector2());
                    }
                    editingTree = true;
                }
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.Label("#" + trees.Count + " Trees");
        EditorGUILayout.EndVertical();
    }

    //search specific directory for all the character files
    void loadCharacters()
    {
        characterList = new List<Character>();
        string[] files = Directory.GetFiles(@"A:\Doujin Circle\Git\Doujin Circle\Assets\json\characters", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(files[i]);
            characterList.Add(JsonHelper<Character>.CreateFromJSON(json));
        }
        Option = new string[characterList.Count];
        for (int cnt = 0; cnt < characterList.Count; cnt++)
        {
            Option[cnt] = characterList[cnt].CharacterName;
        }
    }
}
