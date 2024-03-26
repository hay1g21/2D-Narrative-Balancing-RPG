using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id; //keeps track of id to store collected items

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDesc;

    public string[] pickupText;
    public string[] speakers;

    public bool collectAllowed;

    public List<int> balanceLevels;

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
    }


    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        if (quantity > 1)
        {
            pickupText = new string[] { ("You have found " + quantity.ToString() + " " + itemName + "s") };
        }
        else
        {
            pickupText = new string[] { ("You have found a " + itemName) };
        }
       
        speakers = new string[] { (" ") };
    }
    //remember to find explorer path for each sprite for crediting!
    // Update is called once per frame
    private void Update()
    {
        if (collectAllowed && (Input.GetButtonDown("Collect") || Input.GetButtonDown("Advance")))
            pickUp();
    }


    public void changeBalance(int val)
    {
        //show up or hide here

        if (!balanceLevels.Contains(val))
        {
            transform.Find("Square").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Square").gameObject.SetActive(true);
        }

        GameObject spawns = GameObject.Find("Item" + id);

        //now loop through and find the one it should be in

        //Debug.Log("The value is " + val);
        //Debug.Log("The game manager val is " + GameManager.instance.balanceLevel);
        if(spawns != null)
        {
            foreach (Transform spawn in spawns.transform)
            {
                if (spawn.gameObject.GetComponent<BalanceSpawnPoints>().getSpawns().Contains(val))
                {
                    gameObject.transform.position = spawn.transform.position;
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collectAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collectAllowed = false;
        }
    }

    private void pickUp()
    {
        int extraItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDesc);
        //Debug.Log("Extra items:" + extraItems);
        //check if any remainders, if not destroy
        if (extraItems <= 0)
        {
            Destroy(gameObject);
            inventoryManager.recordID(id);
        }
        else
        {
            quantity = extraItems;
            //Debug.Log("Quantity now:" + quantity);
        }
        GameManager.instance.gameEvents.itemCollected();
        DialogueManager.instance.setText(pickupText,speakers , .03f);
        DialogueManager.instance.dialogueSequence();
    }

    public string getName()
    {
        return itemName;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public Sprite getSprite()
    {
        return sprite;
    }

    public string getItemDesc()
    {
        return itemDesc;
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            int extraItems = inventoryManager.AddItem(itemName, quantity, sprite,itemDesc);
            Debug.Log("Extra items:" + extraItems);
            //check if any remainders, if not destroy
            if(extraItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = extraItems;
                Debug.Log("Quantity now:" + quantity);
            }
        }
       
    }
    */




}
