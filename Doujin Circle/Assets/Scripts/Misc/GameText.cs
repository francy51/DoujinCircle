using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameText 
{

    [SerializeField]
    Language lang;
    [SerializeField]
    string description;
    [SerializeField]
    AudioClip voiceOver;

    public GameText()
    {
        lang = Language.English;
        description = "";
        voiceOver = new AudioClip();
    }


    public Language LanguageName
    {
        get
        {
            return lang;
        }

        set
        {
            lang = value;
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
