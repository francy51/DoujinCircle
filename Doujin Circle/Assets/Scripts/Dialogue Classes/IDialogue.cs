using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{

    int ID
    {
        get;
        set;
    }

    bool HasChoices
    {
        get;
        set;
    }

    string Speech
    {
        get;
        set;
    }

    AudioClip VoiceOver
    {
        get;
        set;
    }

    Character Character
    {
        get;
        set;
    }

    Dialogue[] Choices
    {
        get;
        set;
    }

    Dialogue[] Continuation
    {
        get;
        set;
    }

    Sprite DisplayedPose
    {
        get;
        set;
    }



}
