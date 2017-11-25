using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterImageReference 
{
    public string PoseName;
    public bool UseConstant = true;
    public Sprite ConstantValue;
    public CharacterImageVariable Variable;

    public Sprite Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}
