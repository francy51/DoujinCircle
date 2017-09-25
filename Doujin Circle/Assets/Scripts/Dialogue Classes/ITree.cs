using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITree
{


    string SceneName { get; set; }
    int SceneID { get; set; }
    List<Dialogue> Dialogues { get; set; }


}
