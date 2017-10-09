using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{
    [SerializeField]
    int ID
    {
        get;
        set;
    }
    [SerializeField]
    bool HasChoices
    {
        get;
        set;
    }
    [SerializeField]
    List<GameText> Speech
    {
        get;
        set;
    }
    [SerializeField]
    int Character
    {
        get;
        set;
    }
    [SerializeField]
    List<int> Choices
    {
        get;
        set;
    }
    [SerializeField]
    int DisplayedPoseID
    {
        get;
        set;
    }
    [SerializeField]
    AudioClip SFX { get; set; }
    [SerializeField]
    DialogueActions Action { get; set; }



}
