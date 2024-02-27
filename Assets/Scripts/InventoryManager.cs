using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; //static allows access from anywhere in code, even from other scripts
    public GameObject inventoryMenu;
    public bool activated;
    public ItemSlot[] itemSlots;

    public ItemSO[] itemSOs; //list of all scriptable objects

    public List<int> itemIds = new List<int>(); //list of item ids

    
    // Start is called before the first frame update
    void Start()
    {

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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && activated && !DialogueManager.instance.active)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            activated = false;
        }
        else if (Input.GetButtonDown("Inventory")&& !activated && !DialogueManager.instance.active)
        {
            inventoryMenu.SetActive(true);
            activated = true;
            Time.timeScale = 0;
          
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
