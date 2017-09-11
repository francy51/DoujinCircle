using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameText 
{

    [SerializeField]
    string languageName;
    [SerializeField]
    string description;
    [SerializeField]
    AudioClip voiceOver;

    public GameText()
    {
        languageName = "";
        description = "";
        voiceOver = new AudioClip();
    }


    public string LanguageName
    {
        get
        {
            return languageName;
        }

        set
        {
            languageName = value;
        }
    }

    public string Info
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
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
}
