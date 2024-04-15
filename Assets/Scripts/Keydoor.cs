using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keydoor : MonoBehaviour
{

    public int id;
    [TextArea]
    public string[] diag;
    public string[] faildiag;
    public string hint;
    public string[] speakers = { ("") , ("") , ("") , ("") };
    [SerializeField]
    private bool canOpen;
    public int hintLevel;
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
                GameManager.instance.keyDoorsOpened.Add(id.ToString());
                Destroy(gameObject);

            }
            else{
                DialogueManager.instance.active = true;
                //start dialgoue
                //Debug.Log("Can speak");
                //start dialogue
                if (GameManager.instance.balanceLevel >= hintLevel)
                {
                    string[] hintDiag = new string[faildiag.Length + 1];
                    for (int i = 0; i < faildiag.Length+1; i++)
                    {
                        
                        if(i == faildiag.Length)
                        {
                            hintDiag[i] = hint;
                        }
                        else
                        {
                            hintDiag[i] = faildiag[i];
                        }
                    }
                    DialogueManager.instance.setText(hintDiag, speakers, 0.05f);
                }
                else
                {
                    DialogueManager.instance.setText(faildiag, speakers, 0.05f);
                }
               
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
