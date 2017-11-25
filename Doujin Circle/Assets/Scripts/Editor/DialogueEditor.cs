using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using proj.dialogue;
using System.IO;
using System;

namespace proj.editor
{
	
	public class DialogueEditor : EditorWindow
	{

        Vector2 scroll;
		DialogueTreeVariable tempTree;
		List<DialogueTreeVariable> trees;
		bool editing;
       

		[MenuItem ("Editors/Dialogue")]
		static void Init ()
		{
			// Get existing open window or if none, make a new one:
			DialogueEditor window = (DialogueEditor)EditorWindow.GetWindow (typeof(DialogueEditor));
			window.titleContent.text = "Dialogue Editor";
			window.Show ();

		}

		void OnEnable ()
		{
			LoadTrees ();
		}

		void OnGUI ()
		{
          
			EditorGUILayout.BeginHorizontal ("Box");
			TreeList ();
            MainArea();

			EditorGUILayout.EndHorizontal ();
		}

        void MainArea()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll,"Box");
            GUILayout.Label("The Editor");
            GUILayout.Label("WARNING : if you are editing an already existing tree then don't change the name otherwise it will create a new one.");
            if (GUILayout.Button("New Tree"))
            {
                tempTree = ScriptableObject.CreateInstance<DialogueTreeVariable>();
                editing = true;
            }
            if (editing)
            {
                EditorGUILayout.BeginVertical("Box");
                DisplayTreeDetails();
                if (GUILayout.Button("Save"))
                {
                    JsonHelper<DialogueTreeVariable>.SaveToJson(tempTree, Application.dataPath + "/json/Trees/" + tempTree.TreeName.Value + ".json");
                    tempTree = new DialogueTreeVariable();
                    editing = false;
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }

        private void DisplayTreeDetails()
        {
            tempTree.TreeName.Value = EditorGUILayout.TextField("Tree Name - ", tempTree.TreeName.Value);
            if (tempTree.Start == null)
            {
                if (GUILayout.Button("Add a start"))
                {
                    tempTree.Start = new Dialogue();
                }
            }
            else
            {
                GUILayout.BeginVertical("Box");
                DisplayDialogue(tempTree.Start);
                GUILayout.EndVertical();
            }
        }

        private void DisplayDialogue(Dialogue d)
        {            
            d.Char = (Character)EditorGUILayout.ObjectField(d.Char,typeof(Character));
            if (d.Char != null && d.Char.Poses.Count > 0)
            {
                string[] poses = new string[d.Char.Poses.Count];
                for (int i = 0; i < d.Char.Poses.Count; i++)
                {
                    poses[i] = d.Char.Poses[i].PoseName;
                }
                int poseID = new int();

                poseID = EditorGUILayout.Popup("Sprite Options : ", poseID, poses);

                d.Pose = d.Char.Poses[poseID];
            }
            else
            {
                GUILayout.Label("You have no character or your character has no poses");
                Debug.LogWarning("You have no character or your character has no poses");
            }


            d.Response.Value = EditorGUILayout.TextField("Response - ", d.Response.Value);
            d.PlayerResponse.Value = EditorGUILayout.TextField("PlayerResponse - ", d.PlayerResponse.Value);
        
           


            if (GUILayout.Button("Add Extra Dialogue"))
            {
                d.FollowUp.Add(new Dialogue());
            }
            if (d.FollowUp.Count > 0)
            {
                EditorGUILayout.BeginHorizontal("Box");
                foreach (Dialogue n in d.FollowUp)
                {
                    EditorGUILayout.BeginVertical("Box");
                    DisplayDialogue(n);
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("This is an End Node");
            }
        }

        /// <summary>
        /// Displays a list of all trees
        /// </summary>
        void TreeList ()
		{
            EditorGUILayout.BeginVertical("Box", GUILayout.Width(100f));
            if (trees.Count > 0) 
			{
				
				foreach (DialogueTreeVariable t in trees) 
				{
					if (GUILayout.Button (t.TreeName.Value)) 
					{
						Debug.Log (t.TreeName.Value);
                        tempTree = t;
                        editing = true;
					}
				}
                EditorGUILayout.LabelField(trees.Count.ToString());
         
			} else 
			{
				GUILayout.Label ("No trees yet");
				Debug.Log ("No Trees Yet");
			}
            if (GUILayout.Button("REFRESH"))
            {
                LoadTrees();
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Loads the tree list
        /// </summary>
		void LoadTrees ()
		{
			trees = new List<DialogueTreeVariable> ();
			string[] files = Directory.GetFiles (Application.dataPath + "/json/Trees/", "*.json");
			Debug.Log (Application.dataPath + "/json/Trees/");
			for (int i = 0; i < files.Length; i++) 
			{
				string json = File.ReadAllText (files [i]);
                trees.Add(new DialogueTreeVariable());
                EditorJsonUtility.FromJsonOverwrite(json, trees[i]);
				//trees.Add (JsonHelper<DialogueTreeVariable>.CreateFromJSON (json));
			}
		}

	}
}