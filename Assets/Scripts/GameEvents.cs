using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents
{
    //for picking up an item
    public event Action onItemCollected;


    public void itemCollected()
    {
        if(onItemCollected != null)
        {
            onItemCollected();
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
}