using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{

    Tree curTree;
    Dialogue curDialogue;
    FadeManager fadeMan;
    bool responseNeeder;
    bool displayingDialogue;
    [SerializeField]
    Text characterName;
    [SerializeField]
    Text textArea;
    [SerializeField]
    GameObject dialogueArea;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(dialogueArea);
        fadeMan = GetComponent<FadeManager>();
        curDialogue = new Dialogue();
        curDialogue.Speech = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.";
        curDialogue.Character = new Character();
        curDialogue.Character.CharacterName = "JOHN";
        textArea.text = "";
        Debug.Log(curDialogue.Character.CharacterName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            displayingDialogue = false;
            //and move to next section
            if (curDialogue.HasChoices)
            {

            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //skip
        }
        else
        {
            if (!displayingDialogue)
            {
                characterName.text = curDialogue.Character.CharacterName;
                StartCoroutine(fillTextArea());
                displayingDialogue = true;
            }
        }
    }

    IEnumerator fillTextArea() {
        foreach (char c in curDialogue.Speech)
        {
            
            textArea.text += c;
            //TODO: descision on how fast we want the text to show
            yield return new WaitForEndOfFrame();

        }
    }
}
