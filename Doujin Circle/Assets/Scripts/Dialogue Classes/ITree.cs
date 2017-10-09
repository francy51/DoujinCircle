using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITree
{

    [SerializeField]
    string TreeName { get; set; }
    [SerializeField]
    int SceneID { get; set; }
    [SerializeField]
    List<Dialogue> Dialogues { get; set; }


}
