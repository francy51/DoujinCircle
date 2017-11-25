using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [Tooltip("The Characters Name")]
    public StringReference CharacterName;
    [Tooltip("Short description of the character")]
    public StringReference Description;
    [Tooltip("The Characters age")]
    public IntReference Age;
    /// <summary>
    /// On is male off is female 
    /// </summary>
    /// 
    [Tooltip("On is male and Off is female")]
    public BoolReference Gender;
    [Tooltip("Different poses and images that this character can do")]
    public List<CharacterImageReference> Poses;


   

}
