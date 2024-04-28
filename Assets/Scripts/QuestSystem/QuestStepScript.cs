using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStepScript : MonoBehaviour //inherit
{
    //Code is modified from https://github.com/shapedbyrainstudios/quest-system/blob/3-quest-log-implemented/Assets/Scripts/QuestSystem/QuestStep.cs by trevermock (2023)

    // Start is called before the first frame update
    private bool finished = false;
    private string questId;

    private int stepIndex;

    public void initialiseQuestStep(string questId, int stepIndex)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
    }

    //indicates finishing a step
    protected void finishQuestStep()
    {

        if (!finished)
        {
            finished = true;
            GameManager.instance.gameEvents.advanceQuest(questId);

            //To do - advance quest forweard event
            Destroy(this.gameObject);
        }
    }

    protected void changeState(string newState, string newStatus)
    {
        GameManager.instance.gameEvents.questStepStateChange(questId, stepIndex, new QuestStepState(newState,newStatus));
    }
}
