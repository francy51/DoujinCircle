using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : IDialogue
{

    [SerializeField]
    int id; // Do I need this can I use the list id instead??
    [SerializeField]
    bool playerChoice;
    [SerializeField]
    List<GameText> speech;
    [SerializeField]
    int character;
    [SerializeField]
    List<int> choices;
    [SerializeField]
    int displayedPoseID;
    [SerializeField]
    AudioClip sfx;
    [SerializeField]
    DialogueActions action;
    [SerializeField]
    bool hasContinuation;
    [SerializeField]
    bool isChoice;
    
    public Dialogue()
    {
        id = 0;
        playerChoice = false;
        speech = new List<GameText>();
        choices = new List<int>();
        action = DialogueActions.None;
    }

    public int ID
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public bool PlayerChoice
    {
        get
        {
            return playerChoice;
        }

        set
        {
            playerChoice = value;
        }
    }

    public List<GameText> Speech
    {
        get
        {
            return speech;
        }

        set
        {
            speech = value;
        }
    }



    public int Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }

    public List<int> Choices
    {
        get
        {
            return choices;
        }

        set
        {
            choices = value;
        }
    }

    public int DisplayedPoseID
    {
        get
        {
            return displayedPoseID;
        }

        set
        {
            displayedPoseID = value;
        }
    }

    public AudioClip SFX
    {
        get
        {
            return sfx;
        }

        set
        {
            sfx = value;
        }
    }

    public DialogueActions Action
    {
        get
        {
            return action;
        }

        set
        {
            action = value;
        }
    }

    public bool HasContinuation
    {
        get
        {
            return hasContinuation;
        }

        set
        {
            hasContinuation = value;
        }
    }

    public bool IsChoice
    {
        get
        {
            return isChoice;
        }

        set
        {
            isChoice = value;
        }
    }
}
