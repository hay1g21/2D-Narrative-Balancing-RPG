using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox; //ref to turn on and off the dialogue box

    public static DialogueManager instance; //static allows access from anywhere in code, even from other scripts

    public GameObject player; // to stop player when dialogue is happening

    public bool active = false; //if dialogue is active

    private void Awake()
    {
        if (DialogueManager.instance != null)
        {
            //can destroy other objs here if they dupe
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll(); //deletes data use for debug

        instance = this; //assigns itself to the static var so it can be accessed anywhere
        //DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //dialogueBox.SetActive(false); //hide it
        player = GameObject.Find("Player");
        //dialogueSequence();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void setText(string[] lines, string[] speakers, float speed)
    {
        dialogueBox.GetComponent<DialogueScript>().setDialogue(lines, speakers, speed);
    }

    //for calling a dialogue sequence from different areas
    public void dialogueSequence()
    {

        active = true;
        dialogueBox.SetActive(true);
        dialogueBox.GetComponent<DialogueScript>().StartDialogue();
        Time.timeScale = 0; //the world
    }

   
}
