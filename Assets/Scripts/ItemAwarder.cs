using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAwarder : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDesc;


    //called by cutscene signal
    public void awardItem()
    {
        int extraItems = InventoryManager.instance.AddItem(itemName, quantity, sprite, itemDesc);

        //dialogue manager stuff
        string[] diag =  { ("You have been given " + quantity.ToString() + " " + itemName + "s") };

        if (quantity > 1)
        {
            diag = new string[] { ("You have been given " + quantity.ToString() + " " + itemName + "s") };
        }
        else
        {
            diag = new string[] { ("You have been given a " + itemName) };
            if (itemName.Equals("Pech"))
            {
                diag = new string[] { ("Pech has joined you on your journey and has added himself to your inventory") };
            }
           
        }
        string[] speakers = new string[] { (" ") };
        DialogueManager.instance.setText(diag, speakers, .03f);
        DialogueManager.instance.dialogueSequence();

    }

    
}
