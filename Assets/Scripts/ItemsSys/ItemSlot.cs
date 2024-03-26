using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //Item information
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDesc;

    //size of slot
    [SerializeField]
    int maxItemNum; 

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inManager;


    //Slot info
    [SerializeField]
    private Text qText;

    [SerializeField]
    private Image itemImg;

    public Sprite emptySprite;

    public bool nonConsum; //for nonconsumable items

    //description info
    public Image itemDescImg;
    public Text itemNameText;
    public Text itemDescText;


    //for adding an item to the slot
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDesc)
    {
        //check if the slot is already full
        if (isFull)
        {
            return quantity;
        }

        

        //update vals
        this.itemName = itemName;
       
        this.itemSprite = itemSprite;
        this.itemDesc = itemDesc;

        itemImg.enabled = true; //doesnt work with gameobject instead do component!! REMEMBER NEXT TIME
        itemImg.sprite = itemSprite;
        itemImg.color += new Color(0f, 0f, 0f, 1f);

        //add to amount
        this.quantity += quantity;
        //if oversized
        //Debug.Log("Quantity: " + this.quantity + ", maxItemNum" + maxItemNum);
        if (this.quantity >= maxItemNum)
        {
            qText.text = maxItemNum.ToString();
            qText.enabled = true;
            isFull = true; //now has an item innit


            //return the rest
            int extraItems = this.quantity - maxItemNum;
            this.quantity = maxItemNum;
            return extraItems;
        }

        //update quantity text
        qText.text = this.quantity.ToString();
        qText.enabled = true;

        if (quantity == -1)
        {
            nonConsum = true;
            qText.text = "";

        }
        else
        {
            nonConsum = false;
            
        }

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //OnRightClick();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        inManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void OnLeftClick()
    {
        //if its selected
        if (thisItemSelected)
        {
            Debug.Log("Using " + itemName);
            inManager.useItem(itemName);
            //update quantity visually and logically

            this.quantity -= 1;
            qText.text = this.quantity.ToString();
            if(this.quantity <= 0 && !nonConsum)
            {
                //so it doesn go into negatives
                quantity = 0;
                emptySlot();
            }
            if (nonConsum)
            {
                qText.text = "";
            }
        }
        else
        {
            inManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemNameText.text = itemName;
            itemDescText.text = itemDesc;
            itemDescImg.sprite = itemSprite;
        }


    }
    public void emptySlot()
    {
        //make it reusable
        qText.enabled = false;
        itemImg.sprite = emptySprite;
        itemDesc = "";
        itemName = "";

        itemNameText.text = "";
        itemDescText.text = "";
        isFull = false;

        quantity = 0;
        itemDescImg.sprite = emptySprite;
        itemSprite = emptySprite;
    }


    public void OnRightClick()
    {
      
    }
}
