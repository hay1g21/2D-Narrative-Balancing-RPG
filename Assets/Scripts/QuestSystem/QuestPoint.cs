using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestPoint : MonoBehaviour
{

    private bool isNear = false;

    [SerializeField]
    private QuestSO questPointInfo;

    public GameObject questPointer;

    private string questId;

    private QuestState currQuestState;

    [TextArea]
    public string[] beginDialogue;
    public string[] beginSpeaker; //for a back and forth if necessary

    [TextArea]
    public string[] finishDialogue;
    public string[] finishSpeaker; //for a back and forth if necessary

    [Header("Config")]
    [SerializeField] private bool startPoint = true; //if the thing is a finish point

    [SerializeField] private bool finishPoint = true; //if its an end point

    private void Awake()
    {
        questId = questPointInfo.id;
    }

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onQuestStateChange += QuestStateChange;
        GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onQuestStateChange -= QuestStateChange;
        GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
    }


    

    public void changeBalance(int val)
    {
        //show up or hide here

        if (val < 3)
        {
            transform.Find("Pointer").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Pointer").gameObject.SetActive(false);
        }

        

        //make interaction location wider if on balance level 6
        if (val >= 5)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        }

        //now loop through and find the one it should be in

        //Debug.Log("The value is " + val);
       
    }

    private string accept = "A quest has been added to the log book.";

    // Update is called once per frame
    public void Update()
    {
        if (isNear && ((Input.GetButtonDown("Interact")) || GameManager.instance.balanceLevel >=6) && !DialogueManager.instance.active)
        {

            //start dialgoue
            //Debug.Log("Can speak");
            //start dialogue
            
            //Debug.Log("hello");

            //GameManager.instance.gameEvents.startQuest(questId);
            //GameManager.instance.gameEvents.advanceQuest(questId);
            //GameManager.instance.gameEvents.finishQuest(questId);

            if(currQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                GameManager.instance.gameEvents.startQuest(questId);
                DialogueManager.instance.active = true;
                beginDialogue = beginDialogue.Concat(new string[] { accept }).ToArray();
                beginSpeaker= beginSpeaker.Concat(new string[] { " " }).ToArray();
                DialogueManager.instance.setText(beginDialogue, beginSpeaker, 0.05f);
                DialogueManager.instance.dialogueSequence();
            }
            else if(currQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            {
                GameManager.instance.gameEvents.finishQuest(questId);
                DialogueManager.instance.active = true;
                DialogueManager.instance.setText(finishDialogue, finishSpeaker, 0.05f);
                DialogueManager.instance.dialogueSequence();
            }else{
                Debug.Log("Quest is in state: " + currQuestState);
            }
        }

    }


    private void QuestStateChange(QuestScript quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currQuestState = quest.state;
            //Debug.Log("Quest with id: " + questId + " updated to state: " + currQuestState);
        }
    }
    //for exiting and entering talk zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isNear = false;
        }
    }
}
