using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{

    public string NPCname; //name of the npc

    public bool givesQuest; //if it gives a quest or is just to talk

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

    // Update is called once per frame
    public void Update()
    {
        if (canTalk && (Input.GetButtonDown("Advance")) && !DialogueManager.instance.active)
        {
            DialogueManager.instance.active = true;
            //start dialgoue
            //Debug.Log("Can speak");
            //start dialogue
            DialogueManager.instance.setText(dialogue, speaker, 0.05f);
            DialogueManager.instance.dialogueSequence();
            //Debug.Log("hello");
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
