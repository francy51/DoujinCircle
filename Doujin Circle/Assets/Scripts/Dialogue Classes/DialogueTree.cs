using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : ITree {

    string sceneName;
    int sceneID;
    Dialogue startDialogue;

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

    public Dialogue StartDialogue
    {
        get
        {
            return startDialogue;
        }

        set
        {
            startDialogue = value;
        }
    }
}
