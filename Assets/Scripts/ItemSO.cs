using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO", order = 1)] //creates menu to make object
public class ItemSO : ScriptableObject
{
    //SO: data persists across playthrough
    //also no start and update - All functions called from other methods
    public string itemName; //tells which item is using
    public StatToChange statToChange = new StatToChange(); //adds to inspector??
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange(); //adds to inspector??
    public int amountToChangeAttribute;

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            //now tell how to change health
            
            GameManager.instance.changeHealth(amountToChangeStat);
        }
        if (statToChange == StatToChange.magic)
        {
            //remeber to add ITEMSO to list in canvas
            //now tell how to change health
         
            GameManager.instance.changeMagic(amountToChangeStat);
        }
        if(statToChange == StatToChange.damage)
        {
            //deal some damage
            if (GameManager.instance.gameState.Equals(GameManager.IN_PROGRESS_STATE))
            {
                //GameManager.instance.itemDamage(amountToChangeStat);
                Debug.Log("USE IT");
                GameController.instance.itemDamage(amountToChangeStat);
            }
        }
    }

    public enum StatToChange
    {
        none,
        health,
        magic,
        damage
    };

    public enum AttributeToChange
    {
        none,
        melee,
        magicRange,
        defense,
        speed
    };
}