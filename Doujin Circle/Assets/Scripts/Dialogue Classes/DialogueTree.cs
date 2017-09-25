using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : ITree {

    string sceneName;
    int sceneID;
    List<Dialogue> dialogues;

    public string SceneName
    {
        get
        {
            return sceneName;
        }

        set
        {
            sceneName = value;
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
