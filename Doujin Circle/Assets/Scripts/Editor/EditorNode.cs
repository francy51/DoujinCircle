using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorNode  {

    public int ID;
    [SerializeField]
    public Rect rect;
    [SerializeField]
    public Dialogue dialogue;
    [SerializeField]
    public bool recieved;
    [SerializeField]
    public bool sent;

    public EditorNode(Rect rect, Dialogue dialogue)
    {
        this.rect = rect;
        this.dialogue = dialogue;
    }

    public EditorNode(Rect rect)
    {
        this.rect = rect;
        this.dialogue = new Dialogue();
    }
}
