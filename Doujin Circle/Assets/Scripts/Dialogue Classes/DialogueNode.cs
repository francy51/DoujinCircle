using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode  {
    
    Rect rect;
    Dialogue diag;

    public Rect Rect
    {
        get
        {
            return rect;
        }

        set
        {
            rect = value;
        }
    }

    public Dialogue Diag
    {
        get
        {
            return diag;
        }

        set
        {
            diag = value;
        }
    }

    public DialogueNode( Rect rect, Dialogue diag)
    {
        this.Rect = rect;
        this.Diag = diag;
    }
}
