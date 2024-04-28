using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code is modified from https://github.com/shapedbyrainstudios/quest-system/blob/3-quest-log-implemented/Assets/Scripts/QuestSystem/QuestStepState.cs by trevermock (2023)
[System.Serializable]
public class QuestStepState
{
    public string state;

    public string status;

    public QuestStepState(string state, string status)
    {
        this.state = state;
        this.status = status;
    }

    public QuestStepState()
    {
        this.state = "";
        this.status = "";
    }
}
