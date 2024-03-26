using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{

    public string NPCname; //name of the npc

    public bool givesQuest; //if it gives a quest or is just to talk

    public bool givesItem; //if it gives an item

    //item details
    [SerializeField]
    private string itemName;

    [TextArea]
    [SerializeField]
    private string itemDesc;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite itemSprite;


    public List<int> balanceLevels;

    [SerializeField]
    private bool canTalk;
    [SerializeField]
    private bool isTalking; //when dialogue activates
    [TextArea]
    public string[] dialogue;
    public string[] speaker; //for a back and forth if necessary

    //where the plaer can speak to the npc

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
    }

    public void changeBalance(int val)
    {
        //show up or hide here

        if (!balanceLevels.Contains(val))
        {
            transform.Find("Coll").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Coll").gameObject.SetActive(true);
        }

        GameObject spawns = GameObject.Find(NPCname + "Spawn");

        //now loop through and find the one it should be in

        //Debug.Log("The value is " + val);
        //Debug.Log("The game manager val is " + GameManager.instance.balanceLevel);
        if (spawns != null)
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


    // Update is called once per frame
    public void Update()
    {
        if (canTalk && (Input.GetButtonDown("Advance")) && !DialogueManager.instance.active && balanceLevels.Contains(GameManager.instance.balanceLevel))
        {
            DialogueManager.instance.active = true;
            //start dialgoue
            //Debug.Log("Can speak");
            //start dialogue
            DialogueManager.instance.setText(dialogue, speaker, 0.05f);
            DialogueManager.instance.dialogueSequence();
            //Debug.Log("hello");
            if (givesItem)
            {
                //give the item then stop
                InventoryManager.instance.AddItem(itemName, quantity, itemSprite, itemDesc);
                givesItem = false;
            }
            
        }

    }


    //for exiting and entering talk zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canTalk = false;
        }
    }
}
