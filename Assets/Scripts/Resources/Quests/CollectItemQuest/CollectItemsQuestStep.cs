using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemsQuestStep : QuestStepScript
{
    private int itemsCollected = 0;
    public int itemsToCollect;


    private void Start()
    {
        updateState(); //need to update status right when the step object appears instead of first state change
    }
    private void OnEnable()
    {
        Debug.Log(InventoryManager.instance);
        GameManager.instance.gameEvents.onItemCollected += itemCollected;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onItemCollected -= itemCollected;
    }

    private void itemCollected()
    {
        if (itemsCollected < itemsToCollect)
        {
            
            itemsCollected += 1;
            updateState();
            Debug.Log("ItemCollected");
        }

        if(itemsCollected >= itemsToCollect)
        {
            finishQuestStep();
        }
    }
    //string to rep as state
    private void updateState()
    {
        string state = itemsCollected.ToString();
        string status = "Collected " + itemsCollected + "/" + itemsToCollect;
        changeState(state,status);
    }
}
