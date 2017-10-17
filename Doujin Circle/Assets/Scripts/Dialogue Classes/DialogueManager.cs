using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    List<NodeTree> trees;
    NodeTree curTree;

    private UnityAction onClickActionCall;

    List<Character> characterList;

    public Text textBox;
    public Text nameBox;

    public List<Button> buttons;

    public GameObject dialogueBox;

    FadeManager fadeManager;

    bool canMakeChoice;

    //choice of language
    public Language langSetting;

    [Range(0, 4)]
    public float waitForSeconds;

    public void startTree(int treeID)
    {
        curTree = trees[treeID];
        if (SceneManager.GetActiveScene().buildIndex != curTree.SceneID)
        {
            SceneManager.LoadScene(curTree.SceneID, LoadSceneMode.Single);

        }
        DisplayDialogue(0);
    }

    // Use this for initialization
    void Start()
    {
        loadTrees();
        loadCharacters();
        fadeManager = GetComponent<FadeManager>();
    }

    public void DisplayDialogue(int id)
    {
        Dialogue d = curTree.Dialogues[id];

        nameBox.text = characterList[d.Character].CharacterName;

        for (int i = 0; i < d.Speech.Count; i++)
        {
            if (d.Speech[i].LanguageName == langSetting)
            {
                switch (d.Action)
                {
                    case DialogueActions.None:
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                    case DialogueActions.FadeOutBeforeDialogue:
                        fadeManager.BeginFade(1);
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                    case DialogueActions.FadeOutAfterDialogue:
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        fadeManager.BeginFade(1);
                        break;
                    case DialogueActions.FadeInBeforeDialogue:
                        fadeManager.BeginFade(-1);
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                    case DialogueActions.FadeInAfterDialogue:
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        fadeManager.BeginFade(-1);
                        break;
                    case DialogueActions.PlaySoundEffect:
                        //play sound effect function call
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                    case DialogueActions.ChangeCharacterPose:
                        //change the character position 
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                    default:
                        StartCoroutine(printTextToScreen(d.Speech[i].Info, d));
                        break;
                }



                break;
            }
            foreach (Button b in buttons)
            {
                int index = buttons.FindIndex(a => a == b);
                b.gameObject.SetActive(false);
                if (index == 0 || index < d.Choices.Count)
                {
                    Text txt = b.GetComponentInChildren<Text>();

                    foreach (GameText t in curTree.Dialogues[d.Choices[index]].Speech)
                    {
                        if (t.LanguageName == langSetting)
                        {
                            txt.text = t.Info;
                            b.onClick.AddListener(() => DisplayDialogue(curTree.Dialogues[d.Choices[index]].Choices[0]));
                            b.gameObject.SetActive(true);
                            b.enabled = canMakeChoice;
                            break;
                        }

                    }
                }

            }
        }


    }


    IEnumerator printTextToScreen(string text, Dialogue d)
    {
        string print = "";
        canMakeChoice = false;
        foreach (char c in text)
        {
            print += c;
            textBox.text = print;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                textBox.text = text;
                break;
            }
            yield return new WaitForSeconds(waitForSeconds);
        }
        canMakeChoice = true;

    }

    private void loadTrees()
    {
        trees = new List<NodeTree>();
        string[] files = Directory.GetFiles(@"A:\Doujin Circle\Git\Doujin Circle\Assets\json\trees", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(files[i]);
            trees.Add(JsonHelper<NodeTree>.CreateFromJSON(json));
        }
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

}
