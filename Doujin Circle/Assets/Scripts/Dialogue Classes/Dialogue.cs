using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proj.dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [Tooltip("This is the character that says the line")]
        [SerializeField]
        public Character Char;
        [Tooltip("This is the response that the character gives if the player picks this option")]
        [SerializeField]
        public StringReference Response;
        [Tooltip("This is what apears on the buttons aka the players response. If this is selected this will then trigger the characters response")]
        [SerializeField]
        public StringReference PlayerResponse;
        [Tooltip("Possible next routes of action that the character can take")]
        [SerializeField]
        public List<Dialogue> FollowUp;
        [Tooltip("The pose to be displayed")]
        [SerializeField]
        public CharacterImageReference Pose;

        public Dialogue()
        {
            Char = null;
            Response = new StringReference();
            PlayerResponse = new StringReference();
            FollowUp = new List<Dialogue>();
            Pose = new CharacterImageReference();
        }
    }

}
