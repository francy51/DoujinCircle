using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace proj.dialogue
{
    [CreateAssetMenu]
    public class DialogueTreeVariable : ScriptableObject
    {
        [SerializeField]
        public Dialogue Start;
        [SerializeField]
        public StringReference TreeName;
        [SerializeField]
        public StringReference NextTree;

        public DialogueTreeVariable()
        {
            TreeName = new StringReference();
            NextTree = new StringReference();
        }

        public Dialogue StartTree()
        {
            return Start;
        }

    }
}


