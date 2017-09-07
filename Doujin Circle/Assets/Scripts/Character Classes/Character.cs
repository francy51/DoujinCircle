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
    string description;
    [SerializeField]
    List<Texture2D> poses;
    [SerializeField]
    int gender;

    public Character()
    {
        age = 0;
        characterName = "";
        description = "";
        poses = new List<Texture2D>();
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

    public string Description
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

    public List<Texture2D> Poses
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
}
