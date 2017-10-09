using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeTree : ITree
{

    [SerializeField]
    string treeName;
    [SerializeField]
    int sceneID;
    [SerializeField]
    List<Dialogue> dialogues;

    public NodeTree(string treeName, int sceneID, List<Dialogue> dialogues)
    {
        this.treeName = treeName;
        this.sceneID = sceneID;
        this.dialogues = dialogues;
    }

    public NodeTree()
    {
        this.treeName = "";
        this.sceneID = 0;
        this.dialogues = new List<Dialogue>();
    }

    public string TreeName
    {
        get
        {
            return treeName;
        }

        set
        {
            treeName = value;
        }
    }

    public int SceneID
    {
        get
        {
            return sceneID;
        }

        set
        {
            sceneID = value;
        }
    }

    public List<Dialogue> Dialogues
    {
        get
        {
            return dialogues;
        }

        set
        {
            dialogues = value;
        }
    }
}
