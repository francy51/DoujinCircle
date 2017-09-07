using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow {

    Character tempCharacter;

    bool editChracter;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Editors/Character")]
    static void Init()
    {

        // Get existing open window or if none, make a new one:
        CharacterEditor window = (CharacterEditor)EditorWindow.GetWindow(typeof(CharacterEditor));
        window.Show();

    }

   

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (editChracter)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            tempCharacter.CharacterName = EditorGUILayout.TextField("Name :", tempCharacter.CharacterName);
            tempCharacter.Age = EditorGUILayout.IntField("Age :", tempCharacter.Age);
            tempCharacter.Gender = (int)EditorGUILayout.IntSlider("Gender(0 = Female/ 1 = Male) : ", tempCharacter.Gender, 0, 1);
            tempCharacter.Description = EditorGUILayout.TextArea("Decription :", tempCharacter.Description);
            // work on loading textures
            //for (int i = 0; i < tempCharacter.Poses.Count; i++)
            //{
            //    tempCharacter.Poses[i] = (Texture2D)EditorGUIUtility.GetObjectPickerObject();
            //}
            //if (GUILayout.Button("New Pose"))
            //{
            //    tempCharacter.Poses.Add(new Texture2D(100, 100));
            //}
            EditorGUILayout.EndVertical();
        }
        else
        {
            if (GUILayout.Button("New Character"))
            {
                tempCharacter = new Character();
                editChracter = true;
            }
        }
        EditorGUILayout.EndHorizontal();

    }
}
