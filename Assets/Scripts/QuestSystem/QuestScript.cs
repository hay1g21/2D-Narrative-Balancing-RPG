using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript
{
    public QuestSO info; //all the static data

    // state info

    public QuestState state;

    private int currQuestStepIndex;

    private QuestStepState[] questStepStates; //array of steps in the quests

    //initialise quest
    public QuestScript(QuestSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questSteps.Length]; //make list of steps
        for(int i = 0; i< questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
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
            questStep.initialiseQuestStep(info.id, currQuestStepIndex);
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
    //stores steps
    public void storeQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
            questStepStates[stepIndex].status = questStepState.status;
        }
        else
        {
            Debug.LogWarning("Tried to access quest step but out of range");
        }
    }

    public string getFullStatusText()
    {
        string fullStatus = "";

        if(state == QuestState.REQUIREMENTS_NOT_MET)
        {
            fullStatus = "You aren't able to perform this quest yet.";
        }else if(state == QuestState.CAN_START)
        {
            fullStatus = "This quest can be started!";

        }
        else
        {
            //show prev quests with striketrhoguhts (<s>)
            for(int i = 0; i < currQuestStepIndex; i++)
            {
                fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
            }
            //display curr step
            if (currStepExists())
            {
                fullStatus += questStepStates[currQuestStepIndex].status;
            }
            if(state == QuestState.CAN_FINISH)
            {
                fullStatus += "The quest can be completed.";
            }else if(state == QuestState.FINISHED)
            {
                fullStatus += "The quest has been completed!";
            }
        }
        return fullStatus;
    }
}
