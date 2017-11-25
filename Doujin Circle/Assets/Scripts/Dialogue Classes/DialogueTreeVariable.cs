using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proj.dialogue
{
	[CreateAssetMenu]
	public class DialogueTreeVariable : ScriptableObject
	{
        [SerializeField]
		public Dialogue Start;
        [SerializeField]
		public StringReference TreeName;

        public DialogueTreeVariable()
        {
            TreeName = new StringReference();
        }

        public Dialogue StartTree ()
		{
			return Start;	
		}
        
	}
}


