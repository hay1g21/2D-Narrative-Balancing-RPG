using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{

    private bool isNear = false;

    [SerializeField]
    private QuestSO questPointInfo;

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
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onQuestStateChange -= QuestStateChange;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isNear && (Input.GetButtonDown("Interact")) && !DialogueManager.instance.active)
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
