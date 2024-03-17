using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents
{
    //defeating enemies
    public event Action<string> onEnemyKilled;


    public void enemyKilled(string enemyType)
    {
        if (onEnemyKilled != null)
        {
            onEnemyKilled(enemyType);
        }
    }
    //for picking up an item
    public event Action onItemCollected;


    public void itemCollected()
    {
        if(onItemCollected != null)
        {
            onItemCollected();
        }
    }
    //for using an item
    public event Action onItemUsed;
   
    public void itemUsed()
    {
        if (onItemUsed != null)
        {
            onItemUsed();
        }
    }

    //for a cutscene
    public event Action<int> onCutSceneTrigger;

    public void startCutscene(int id)
    {
        if (onCutSceneTrigger != null)
        {
            onCutSceneTrigger(id);
        }
    }

    //For exp
    public event Action<int> onExpCollected;

    public void collectExp(int amount)
    {
        if (onExpCollected != null)
        {
            onExpCollected(amount);
        }
    }
    //FOR QUESTS

    public event Action<string> onStartQuest; //string is the quest id

    //function to activate event
    public void startQuest(string id)
    {
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest; //string is the quest id

    //function to activate event
    public void advanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest; //string is the quest id

    //function to activate event
    public void finishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<QuestScript> onQuestStateChange; //string is the quest id

    //function to activate event
    public void questStateChange(QuestScript quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }

    //to track individual quest step state
    public event Action<string, int, QuestStepState> onQuestStepsStateChange; //string is the quest id

    //function to activate event
    public void questStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepsStateChange != null)
        {
            onQuestStepsStateChange(id, stepIndex, questStepState);
        }
    }

    //########FOR SLIDER###########

    public event Action<int> onSliderStepChange; //int is the value of slider, from 0-6 (max play to max narr)

    public void sliderStepChange(int val)
    {
        if (onSliderStepChange != null)
        {
            onSliderStepChange(val);
        }
    }
}
