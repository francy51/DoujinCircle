using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace proj.dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        public Text textBox;

        public bool displayText;

        public bool started;

        public Button[] buttons;

        [SerializeField]
        Dialogue dialogue;

        public DialogueTreeVariable Tree;

        Queue<Dialogue> queue;

        private void Start()
        {
            foreach (Button b in buttons)
            {
                b.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (displayText)
            {
                if (queue.Count == 0)
                {
                    queue.Enqueue(Tree.Start);
                    started = true;
                }
                dialogue = queue.Dequeue();
                TextToDisplay();
            }

        }

        void TextToDisplay()
        {
            StartCoroutine(dialogue.Response.Value);
            for (int i = 0; i < dialogue.FollowUp.Count; i++)
            {
                buttons[i].GetComponentInChildren<Text>().text = dialogue.FollowUp[i].PlayerResponse.Value;
            }
        }

        public void button1()
        {
            queue.Enqueue(dialogue.FollowUp[1]);
        }

        public void button2()
        {
            queue.Enqueue(dialogue.FollowUp[2]);
        }

        public void button3()
        {
            queue.Enqueue(dialogue.FollowUp[3]);
        }

        public void button4()
        {
            queue.Enqueue(dialogue.FollowUp[4]);
        }

        public IEnumerator DisplayText(string text)
        {
            textBox.text += "";
            foreach (char c in text)
            {
                textBox.text += text;
                //TODO: Make some sort of global file where the wait time between characters cna be changed automatically for all text
                yield return new WaitForSeconds(5);
            }
            textBox.text += "\r\n ->";
        }

    }
}


