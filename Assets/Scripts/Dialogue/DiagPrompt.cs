using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagPrompt : MonoBehaviour
{
  

    public bool touched = false;
    

    public int id; //Note id for spawn pos

    //for dialogue
    public string[] speeches;
    public string[] speakers;

    public List<int> balanceLevels;




    private void OnEnable()
    {
        GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
    }


    void Start()
    {
        //make it invis
        SpriteRenderer ren = gameObject.GetComponent<SpriteRenderer>();
        ren.color = new Vector4(ren.color.r, ren.color.g, ren.color.b, 0);


    }
    
    public void changeBalance(int val)
    {

        //move to correct spawn point

        GameObject spawns = GameObject.Find("Prompt" + id);

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

    //used to store any colliders of an object touching the level exit's trigger region
    void OnTriggerEnter2D(Collider2D collision)
    {

        //checks if the object has the player tag
        if (collision.gameObject.CompareTag("Player") && !touched && !GameManager.instance.cutScenePlaying && balanceLevels.Contains(GameManager.instance.balanceLevel))
        {
            touched = true;
            //if it does, the player has touched the exit and the next level is loaded
            DialogueManager.instance.setText(speeches, speakers, 0.03f);
            DialogueManager.instance.dialogueSequence();
            GameManager.instance.dialogueBoxes.Add(id.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
