using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpecificDialogueEditor : EditorWindow
{

    public Dialogue tempDiag;

    public SpecificDialogueEditor(Dialogue diag)
    {
        tempDiag = diag;
    }

    //void Init()
    //{
    //    SpecificDialogueEditor window = (SpecificDialogueEditor)EditorWindow.GetWindow(typeof(SpecificDialogueEditor));
    //    window.titleContent.text = "Dialogue Tree Editor";
    //    window.Show();
    //}

    void OnGUI()
    {
        GUILayout.Label("Working");
    }
}
