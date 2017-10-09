using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : IDialogue
{

    [SerializeField]
    int id; // Do I need this can I use the list id instead??
    [SerializeField]
    bool hasChoices;
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
    
    public Dialogue()
    {
        id = 0;
        hasChoices = false;
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

    public bool HasChoices
    {
        get
        {
            return hasChoices;
        }

        set
        {
            hasChoices = value;
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
}
