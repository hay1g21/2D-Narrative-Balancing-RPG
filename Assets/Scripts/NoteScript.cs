using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
   

    

    public string[] speeches;
    public string[] speakers;

    public List<int> balanceLevels;

   

    public int id; //Note id for spawn pos
   
    public bool collectAllowed;

    private InventoryManager inventoryManager;

    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        if(GameManager.instance != null)
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

    //remember to find explorer path for each sprite for crediting!
    // Update is called once per frame
    private void Update()
    {
        if (collectAllowed && (Input.GetButtonDown("Collect") || Input.GetButtonDown("Advance")) && !DialogueManager.instance.active && balanceLevels.Contains(GameManager.instance.balanceLevel))
            pickUp();

        
    }

    public bool automated;

    public void changeBalance(int val)
    {

        

        if (!balanceLevels.Contains(val))
        {
            transform.Find("Square").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Square").gameObject.SetActive(true);
        }

        //move to correct spawn point

        GameObject spawns = GameObject.Find("Note" + id);

        //now loop through and find the one it should be in

        //Debug.Log("The value is " + val);
        //Debug.Log("The game manager val is " + GameManager.instance.balanceLevel);

        if (automated)
        {
           Transform spawn1 = null;
           Transform spawn2 = null;
          
           spawn1 = GameObject.Find("Note" + id + "/" + "Ludic").transform;
           spawn2 = GameObject.Find("Note" + id + "/" + "Narrative").transform;
           
           

           Vector3 distance = (spawn1.position - spawn2.position)/(balanceLevels.Count-1);

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
        
        DialogueManager.instance.setText(speeches, speakers, .03f);
        DialogueManager.instance.dialogueSequence();
    }
}
