using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStepScript : MonoBehaviour //inherit
{
    // Start is called before the first frame update
    private bool finished = false;
    private string questId;

    public void initialiseQuestStep(string questId)
    {
        this.questId = questId;
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
}
