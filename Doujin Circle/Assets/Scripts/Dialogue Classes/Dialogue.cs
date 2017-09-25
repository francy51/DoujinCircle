using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : IDialogue
{

    int id;
    bool hasChoices;
    string speech;
    AudioClip voiceOver;
    Character character;
    List<Dialogue> choices;
    Dialogue continuation;
    Sprite displayedPose;

    public Dialogue()
    {
        speech = "";

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

    public string Speech
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

    public AudioClip VoiceOver
    {
        get
        {
            return voiceOver;
        }

        set
        {
            voiceOver = value;
        }
    }

    public Character Character
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

    public List<Dialogue> Choices
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

    public Dialogue Continuation
    {
        get
        {
            return continuation;
        }

        set
        {
            continuation = value;
        }
    }

    public Sprite DisplayedPose
    {
        get
        {
            return displayedPose;
        }

        set
        {
            displayedPose = value;
        }
    }
}
