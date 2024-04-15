using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChoiceSO", menuName = "ScriptableObjects/DialogueChoiceSO", order = 1)] //creates menu to make object
public class DialogueChoiceSO : ScriptableObject
{

    public string id; //tells which quest it is
    public string relatedEnemy; //tells which quest it is

    [TextArea]
    public string dialogueChoice;

    public OptionQuality optionQual = new OptionQuality(); //adds to inspector??

    public enum OptionQuality
    {
        none,
        bad,
        medium,
        good
    };

    [TextArea]
    public string attackDesc;


}
