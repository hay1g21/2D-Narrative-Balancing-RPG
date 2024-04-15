using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSisterQuestStep : QuestStepScript
{
    public bool meetNPC;
    public string npcName;

    private void Start()
    {
        updateState(); //need to update status right when the step object appears instead of first state change
    }
    private void OnEnable()
    {
        Debug.Log(InventoryManager.instance);
        GameManager.instance.gameEvents.onNPCTalkedTo += NPCFound;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onNPCTalkedTo -= NPCFound;
    }

    private void NPCFound(string talkedToName)
    {
        if (npcName.Equals(talkedToName))
        {
            meetNPC = true;
          
            updateState();
            finishQuestStep();
            Debug.Log("Correct NPC Talked TO");
        }

       
    }
    //string to rep as state
    private void updateState()
    {
        if (meetNPC)
        {
            string state = "Talked to sister";
            string status = "Talked to his sister. She is safe and hiding in the crypt. Her name is " + npcName;
            changeState(state, status);
        }
        else
        {
            string state = "Need to find" + npcName;
            string status = "A man asked you to find his sister, somewhere in the crypt.";
            changeState(state, status);
        }
       
    }
}
