using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keydoor : MonoBehaviour
{

    [TextArea]
    public string[] diag;
    public string[] faildiag;
    public string[] speakers = { ("") };
    [SerializeField]
    private bool canOpen;
    public bool destroyable; //determines whetehr key is destroyed or not
    //[SerializeField]
    //private bool isOpen;

   
    public string reqItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && (Input.GetButtonDown("Advance")) && !DialogueManager.instance.active)
        {
            //try and open
            
            //Debug.Log("hello");
            if (InventoryManager.instance.inInvent(reqItem))
            {
                //can enter, disable coll
                DialogueManager.instance.active = true;
                //start dialgoue
                //Debug.Log("Can speak");
                //start dialogue
                DialogueManager.instance.setText(diag, speakers, 0.05f);
                DialogueManager.instance.dialogueSequence();
                if (destroyable)
                {
                    InventoryManager.instance.destroyItem(reqItem);
                }
                Destroy(gameObject);

            }
            else{
                DialogueManager.instance.active = true;
                //start dialgoue
                //Debug.Log("Can speak");
                //start dialogue
                DialogueManager.instance.setText(faildiag, speakers, 0.05f);
                DialogueManager.instance.dialogueSequence();
            }
            

        }
    }

    


    //for exiting and entering talk zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canOpen = false;
        }
    }

}
