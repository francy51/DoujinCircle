using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character 
{
    [SerializeField]
    string characterName;
    [SerializeField]
    int age;
    [SerializeField]
    List<GameText> languages;
    [SerializeField]
    List<Sprite> poses;
    [SerializeField]
    int gender;
    [SerializeField]
    string japaneseName;

    public Character()
    {
        age = 0;
        characterName = "";
        Languages = new List<GameText>();
        JapaneseName = "";
        poses = new List<Sprite>();
        gender = 0;
    }

    public string CharacterName
    {
        get
        {
            return characterName;
        }

        set
        {
            characterName = value;
        }
    }

    public int Age
    {
        get
        {
            return age;
        }

        set
        {
            age = value;
        }
    }
    

    public List<Sprite> Poses
    {
        get
        {
            return poses;
        }

        set
        {
            poses = value;
        }
    }

    public int Gender
    {
        get
        {
            return gender;
        }

        set
        {
            gender = value;
        }
    }

    public List<GameText> Languages
    {
        get
        {
            return languages;
        }

        set
        {
            languages = value;
        }
    }

    public string JapaneseName
    {
        get
        {
            return japaneseName;
        }

        set
        {
            japaneseName = value;
        }
    }
}
