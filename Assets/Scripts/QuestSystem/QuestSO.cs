using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code is modified from https://github.com/shapedbyrainstudios/quest-system/blob/3-quest-log-implemented/Assets/Scripts/QuestSystem/QuestInfoSO.cs by trevermock (2023)

[System.Serializable]
[CreateAssetMenu(fileName = "QuestSO", menuName = "ScriptableObjects/QuestSO", order =1)] //creates menu to make object
public class QuestSO : ScriptableObject
{
    //SO: data persists across playthrough
    //SOs are best to hold static non changing data
    //also no start and update - All functions called from other methods

    public string id; //tells which quest it is

    public string displayName; //questname

    public int playerLevelReq; //level requirement

    public QuestSO[] questPrereqs; //pointles sbut


    public GameObject[] questSteps; //steps before quest

    public int goldReward;
    public int expReward;


    //make id name of SO
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
