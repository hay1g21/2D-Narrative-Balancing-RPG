using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
   

    

    public string[] speeches;
    public string[] speakers;

    public bool collectAllowed;

    private InventoryManager inventoryManager;

    void Start()
    {
        
        
    }
    //remember to find explorer path for each sprite for crediting!
    // Update is called once per frame
    private void Update()
    {
        if (collectAllowed && (Input.GetButtonDown("Collect") || Input.GetButtonDown("Advance")) && !DialogueManager.instance.active)
            pickUp();
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
