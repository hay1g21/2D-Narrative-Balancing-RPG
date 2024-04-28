using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   
   Adapted from:
   "Part 7: Scriptable Items -- Let's Make An Inventory System In Unity!" by Night Run Studio (YouTube, 2023)
   Video URL: https://www.youtube.com/watch?v=IbybLKqq7PA
*/


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

    public SpecialUses specialUses = new SpecialUses();

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
        if(specialUses == SpecialUses.pech)
        {
            GameManager.instance.pechToolTips();
            Debug.Log("Pech");
        }
    }

    //represent stats in string form
    public string parseItem()
    {
        string itemParsed = "";

        switch (statToChange)
        {
            case StatToChange.health:
                itemParsed += "Health Recovery: ";
                break;
            case StatToChange.magic:
                itemParsed += "Magic Recovery: ";
                break;
            case StatToChange.damage:
                itemParsed += "Attack Damage: ";
                break;
            case StatToChange.none:
                break;
        }

        if(amountToChangeStat !=  0)
        {
            itemParsed += amountToChangeStat;
            itemParsed += "\n";
        }
      

        switch (specialUses)
        {
            case SpecialUses.pech:
                itemParsed += "Gives enemy details when used in battle.";
                break;
            case SpecialUses.key:
                itemParsed += "Opens a door nearby.";
                break;
            case SpecialUses.story:
                itemParsed += "No effect";
                break;

        }

        return itemParsed;


    }

    //REMEMEBER TO ADD NEW ITEM TO THE LIST OF ITEMS IN INVENT MANAGER
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

    public enum SpecialUses {

        none,
        key,
        story,
        pech
    }
}