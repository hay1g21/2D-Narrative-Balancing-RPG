using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemsQuestStep : QuestStepScript
{
    private int itemsCollected = 0;
    public int itemsToCollect;

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
            Debug.Log("ItemCollected");
        }

        if(itemsCollected >= itemsToCollect)
        {
            finishQuestStep();
        }
    }
}
