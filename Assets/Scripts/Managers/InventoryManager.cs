using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
   
   Adapted from:
   "Part 1: UI Creation and Game Pause -- Let's Make An Inventory System in Unity!" by Night Run Studio (YouTube, 2023)
   Video URL: https://www.youtube.com/watch?v=LaQp5u0_UYk
*/

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; //static allows access from anywhere in code, even from other scripts
    public GameObject inventoryMenu;
    public bool activated;
    public ItemSlot[] itemSlots;

    public ItemSO[] itemSOs; //list of all scriptable objects

    public List<int> itemIds = new List<int>(); //list of item ids

    public Sprite pechSprite;
    // Start is called before the first frame update
    void Start()
    {
        //add special item pech to the thign
        //AddItem("Pech",-1,pechSprite,"The spirit of the necromancer who doomed the world. Lives in Bones' Pouch. His astute observations help provide information about enemies.");
    }


    //so it persists
    private void Awake()
    {
        if (InventoryManager.instance != null)
        {
            //can destroy other objs here if they dupe
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll(); //deletes data use for debug

        instance = this; //assigns itself to the static var so it can be accessed anywhere
        DontDestroyOnLoad(gameObject);

        
    }

    //force the menu to close. Useful for combat
    public void forceClosed()
    {
        Time.timeScale = 1;
        inventoryMenu.SetActive(false);
        activated = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && activated && !DialogueManager.instance.active)
        {
            if (!GameManager.instance.gameState.Equals(GameManager.IN_PROGRESS_STATE))
            {
                Time.timeScale = 1;
                inventoryMenu.SetActive(false);
                activated = false;
            }
            else
            {
                if (GameController.instance.canAttack)
                {
                    Time.timeScale = 1;
                    inventoryMenu.SetActive(false);
                    activated = false;
                }
            }  
        }
        else if (Input.GetButtonDown("Inventory")&& !activated && !DialogueManager.instance.active)
        {
            if (!GameManager.instance.gameState.Equals(GameManager.IN_PROGRESS_STATE))
            {
                inventoryMenu.SetActive(true);
                activated = true;
                Time.timeScale = 0;
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if (itemSlots[i].thisItemSelected)
                    {
                        itemSlots[i].updateDesc();
                        break;
                    }
                }
            }
            else
            {
                if (GameController.instance.canAttack)
                {
                    inventoryMenu.SetActive(true);
                    activated = true;
                    Time.timeScale = 0;
                }
            }

        }
    }
    //called when used
    public void useItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
           
            if (itemSOs[i].itemName.Equals(itemName))
            {

              
                itemSOs[i].UseItem();
                if (GameManager.instance.gameState.Equals(GameManager.IN_PROGRESS_STATE))
                {
                    //send event so gamecontroller knwos a turn has been used
                    GameManager.instance.gameEvents.itemUsed();
                }
            }
            
        }
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDesc)
    {
        //Debug.Log("Its working");
        for(int i = 0; i < itemSlots.Length; i++)
        {
            //goes through eahc slot to find an empty one and adds like filling in a block
            //checks also if the items are of the same type, or if the item slot has free space
            if(itemSlots[i].isFull == false && itemSlots[i].itemName.Equals(itemName) || itemSlots[i].quantity == 0 )
            {
                //Debug.Log("Slot " + i);
                int leftOvers = itemSlots[i].AddItem(itemName, quantity, itemSprite,itemDesc);
                //Debug.Log(leftOvers);
                if(leftOvers > 0)
                {
                    leftOvers = AddItem(itemName, leftOvers, itemSprite, itemDesc);
                   
                }
                return leftOvers;

            }
        }
        return quantity;
    }
    //check if the item is in their inventory
    public bool inInvent(string itemName)
    {
        bool yes = false;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemName.Equals(itemName)){
                yes = true;
            }
        }
        return yes; 
    }

    public string getItemStats(string itemName)
    {
        ItemSO item = null;

        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName.Equals(itemName))
            {
                item = itemSOs[i];
                break;
            }
        }

        if (item != null) return item.parseItem(); else return "";
    }


    public void destroyItem(string itemName)
    {

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemName.Equals(itemName))
            {
                itemSlots[i].emptySlot();
                break;
            }
        }
        
    }

    public void DeselectAllSlots()
    {
        //double tab to gen syntax
        for (int i = 0; i < itemSlots.Length; i++)
        {
            
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
        }
    }

    public void recordID(int id)
    {
        itemIds.Add(id);
    }
}
