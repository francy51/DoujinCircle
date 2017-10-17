using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class CharacterEditor : EditorWindow
{

    Character tempCharacter;
    

    List<Character> characterList;

    bool editChracter;

    Vector2 scrollPoses;
    Vector2 scrollList;
    Vector2 scrollMaster;
    Vector2 scrolLanguages;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Editors/Character")]
    static void Init()
    {

        // Get existing open window or if none, make a new one:
        CharacterEditor window = (CharacterEditor)EditorWindow.GetWindow(typeof(CharacterEditor));
        window.titleContent.text = "Character Editor";
        window.Show();

    }

    void OnEnable()
    {
        loadCharacters();

    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        listCharacters();
        if (editChracter)
        {
            scrollMaster = EditorGUILayout.BeginScrollView(scrollMaster);
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            tempCharacter.CharacterName = EditorGUILayout.TextField("English Name :", tempCharacter.CharacterName);
            tempCharacter.JapaneseName = EditorGUILayout.TextField("Japanese Name :", tempCharacter.JapaneseName);
            tempCharacter.Age = EditorGUILayout.IntField("Age :", tempCharacter.Age);
            tempCharacter.Gender = (int)EditorGUILayout.IntSlider("Gender(0 = Female/ 1 = Male) : ", tempCharacter.Gender, 0, 1);

            //scrollTextArea = EditorGUILayout.BeginScrollView(scrollTextArea);
            EditorGUILayout.BeginVertical();
            scrolLanguages = EditorGUILayout.BeginScrollView(scrolLanguages);
            for (int i = 0; i < tempCharacter.Languages.Count; i++)
            {
                tempCharacter.Languages[i].LanguageName = (Language)EditorGUILayout.EnumPopup("Languge Name : ", tempCharacter.Languages[i].LanguageName);
                tempCharacter.Languages[i].Info = EditorGUILayout.TextField("Info :",tempCharacter.Languages[i].Info,GUILayout.Height(200f));
                tempCharacter.Languages[i].VoiceOver = (AudioClip)EditorGUILayout.ObjectField("Voice over for " + tempCharacter.Languages[i].LanguageName, tempCharacter.Languages[i].VoiceOver, typeof(AudioClip), false);
            }
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("New language"))
            {
                tempCharacter.Languages.Add(new GameText());
            }
            EditorGUILayout.EndVertical();
            GUILayout.Label("#" + tempCharacter.Languages.Count + " languages");
            //EditorGUILayout.EndScrollView();


            //work on loading textures
            scrollPoses = EditorGUILayout.BeginScrollView(scrollPoses);
            for (int i = 0; i < tempCharacter.Poses.Count; i++)
            {
                tempCharacter.Poses[i] = (Sprite)EditorGUILayout.ObjectField("Pose N. " + (i + 1), tempCharacter.Poses[i], typeof(Sprite), false);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Label("#" + tempCharacter.Poses.Count + " poses");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("New Pose"))
            {
                tempCharacter.Poses.Add(new Sprite());
            }
            if (GUILayout.Button("Save"))
            {
                if (tempCharacter.CharacterName == "" || tempCharacter.Age <= 0)
                {

                    Debug.Log("Character has no name or was not given an age");

                }
                else
                {
                    JsonHelper<Character>.SaveToJson(tempCharacter, @"A:\Doujin Circle\Git\Doujin Circle\Assets\json\Characters\" + tempCharacter.CharacterName + ".json");
                    editChracter = false;
                    loadCharacters();
                }

            }
            if (GUILayout.Button("Cancel"))
            {
                Debug.Log("Canceled actions nothing saved");
                editChracter = false;
                loadCharacters();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }
        else
        {
            if (GUILayout.Button("New Character"))
            {
                tempCharacter = new Character();
                editChracter = true;
            }
            if (tempCharacter != null)
            {
                if (GUILayout.Button("Restore"))
                {
                    editChracter = true;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

    }


    //search specific directory for all the character files
    void loadCharacters()
    {
        characterList = new List<Character>();
        string[] files = Directory.GetFiles(@"A:\Doujin Circle\Git\Doujin Circle\Assets\json\characters", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(files[i]);
            characterList.Add(JsonHelper<Character>.CreateFromJSON(json));
        }
    }


    //display all the created character in a list
    private void listCharacters()
    {
        EditorGUILayout.BeginVertical();
        scrollList = EditorGUILayout.BeginScrollView(scrollList);
        if (characterList.Count <= 0)
        {
            GUILayout.Label("No Characters make one");
        }
        else
        {
            //Add some other way such as showing sprites to identify the characters
            foreach (Character c in characterList)
            {

                if (GUILayout.Button(c.CharacterName))
                {
                    tempCharacter = c;
                    editChracter = true;
                }
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.Label("#" + characterList.Count + " characters");
        EditorGUILayout.EndVertical();
    }
}
