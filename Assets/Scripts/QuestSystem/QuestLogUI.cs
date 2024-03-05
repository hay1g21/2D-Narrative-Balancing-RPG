using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [SerializeField] private QuestLogScrollingList scrollingList;

    [SerializeField] private TextMeshProUGUI questDisplayNameText;

    [SerializeField] private TextMeshProUGUI questStatusText;

    [SerializeField] private TextMeshProUGUI goldRewardsText;

    [SerializeField] private TextMeshProUGUI expRewardText;

    [SerializeField] private TextMeshProUGUI levelRequirementsText;
    [SerializeField] private TextMeshProUGUI questRequirementText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onQuestStateChange += questStateChange;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onQuestStateChange -= questStateChange;
    }


    private void questStateChange(QuestScript quest)
    {
        //returns new button or existing button
        QuestLogButton questLogButt = scrollingList.createButtIfNotExist(quest, () =>
        {
            setQuestLogInfo(quest); //objs in c# are passed by ref, so refers to one with most up to date state
        });

        //selects button if its the first button made
        if(firstSelectedButton == null)
        {
            firstSelectedButton = questLogButt.button;
            //firstSelectedButton.Select();
        }
        questLogButt.SetState(quest.state);
    }

    private void QuestLogTogglePressed()
    {
        if (contentParent.activeInHierarchy)
        {

            
            contentParent.SetActive(false);
           // Time.timeScale = 1; //breaks key inputs btdubs
            EventSystem.current.SetSelectedGameObject(null); //reselects next time it is open
        }
        else
        //show
        {
            contentParent.SetActive(true);
            //Time.timeScale = 0;
            if (firstSelectedButton != null)
            {
                firstSelectedButton.Select();
                //firstSelectedButton.Select();
            }
            
           
        }
        GameManager.instance.player.GetComponent<PlayerMovement>().switchMovement();

    }
    //open menu
    private void Update()
    {
        if (Input.GetButtonDown("QuestMenu") && !DialogueManager.instance.active)
        {
            QuestLogTogglePressed();
        }

    }
    //set everything with the quest ui, called when button is selected
    private void setQuestLogInfo(QuestScript quest)
    {
        questDisplayNameText.text = quest.info.displayName;
        //gets full status text
        questStatusText.text = quest.getFullStatusText(); 
        // Todo with satatus

        //reqs
        levelRequirementsText.text = "Level " + quest.info.playerLevelReq;
        questRequirementText.text = "";
        foreach(QuestSO prereqQuestInfo in quest.info.questPrereqs)
        {
            questRequirementText.text += prereqQuestInfo.displayName + "\n";
        }

        goldRewardsText.text = quest.info.goldReward + " Gold";
        expRewardText.text = quest.info.expReward + " Exp";
    }
}
