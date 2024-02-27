using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript
{
    public QuestSO info; //all the static data

    // state info

    public QuestState state;

    private int currQuestStepIndex;

    //initialise quest
    public QuestScript(QuestSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currQuestStepIndex++ ;
    }

    public bool currStepExists()
    {
        return (currQuestStepIndex < info.questSteps.Length);
    }
    //adds a current quest to a transform
    public void InstantiateCurrQuestStep(Transform parentTrans)
    {
        GameObject questStepPF = getCurrQuestStepPF();
        //creates object if exists from list of steps
        if(questStepPF != null)
        {
            //Object.Instantiate<GameObject>(questStepPF, parentTrans);
            QuestStepScript questStep = Object.Instantiate<GameObject>(questStepPF, parentTrans).GetComponent<QuestStepScript>();
            questStep.initialiseQuestStep(info.id);
        }
    }

    private GameObject getCurrQuestStepPF()
    {
        GameObject questStepPF = null;
        if (currStepExists()){
            questStepPF = info.questSteps[currQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range meaning no current step:"
                + "Quest ID: " + info.id + ", stepIndex = " + currQuestStepIndex);
        }
        return questStepPF;
    }
}
