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
    private bool firstTalk;
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
        if (GameManager.instance != null)
        {
            GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
        }
    }

    private void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
        }
    }

    public bool automated;

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

        //make interaction location wider if on balance level 6
        if(val >= 5)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        }

        //now loop through and find the one it should be in

        //Debug.Log("The value is " + val);
        //Debug.Log("The game manager val is " + GameManager.instance.balanceLevel);

        if (automated)
        {
            Transform spawn1 = null;
            Transform spawn2 = null;

            spawn1 = GameObject.Find(NPCname + "Spawn" + "/" + "Ludic").transform;
            spawn2 = GameObject.Find(NPCname + "Spawn" + "/" + "Narrative").transform;



            Vector3 distance = (spawn1.position - spawn2.position) / (balanceLevels.Count - 1);

            Vector3 newDistance = spawn1.position - distance * val;
            gameObject.transform.position = newDistance;
        }
        else
        {
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

        
    }


    // Update is called once per frame
    public void Update()
    {
        if (GameManager.instance != null)
        {
            if ((Input.GetButtonDown("Advance") || (GameManager.instance.balanceLevel >= 6 && !firstTalk)) && canTalk && !DialogueManager.instance.active && balanceLevels.Contains(GameManager.instance.balanceLevel))
            {
                GameManager.instance.gameEvents.npcTalkedTo(NPCname);
                firstTalk = true;
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
