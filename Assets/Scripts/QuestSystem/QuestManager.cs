using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, QuestScript> questMap;

    // quest start req

    public int currPlayerLevel; //change later to adapt to level

    public static QuestManager instance; //static allows access from anywhere in code, even from other scripts

    private void Awake()
    {
        questMap = CreateQuestMap();

        //QuestScript quest = getQuestById("CollectItemsQuest");
        //Debug.Log(quest.info.displayName); //info is the SO
        //Debug.Log(quest.state);

        if (QuestManager.instance != null)
        {
            //can destroy other objs here if they dupe
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll(); //deletes data use for debug

        instance = this; //assigns itself to gamemanager object in the scene
        //sceneloaded is an event which fires from scenemanager when scene is loadedgameEvents = new GameEvents();

        DontDestroyOnLoad(gameObject);
     

    }

    public void OnEnable()
    {
        //subscribe to events
        GameManager.instance.gameEvents.onStartQuest += startQuest;
        GameManager.instance.gameEvents.onAdvanceQuest += advanceQuest;
        GameManager.instance.gameEvents.onFinishQuest += finishQuest;

        GameManager.instance.gameEvents.onQuestStepsStateChange += questStepStateChange;
       
    }

    private void OnDisable()
    {
        //unsub
        GameManager.instance.gameEvents.onStartQuest -= startQuest;
        GameManager.instance.gameEvents.onAdvanceQuest -= advanceQuest;
        GameManager.instance.gameEvents.onFinishQuest -= finishQuest;

        GameManager.instance.gameEvents.onQuestStepsStateChange -= questStepStateChange;
    }

    //just so questpoints know where they are at
    public void UpdateQuestPoints()
    {
        //broadcast the state of all quests on startup
        foreach (QuestScript qs in questMap.Values)
        {
            changeQuestState(qs.info.id, qs.state);
        }
    }
    private void Start()
    {
        //broadcast the state of all quests on startup
        foreach  (QuestScript quest in questMap.Values)
        {
            GameManager.instance.gameEvents.questStateChange(quest);
        }
        Debug.Log("Did it understand");
    }

    //constantly checks if quests can start
    private void Update()
    {
        foreach (QuestScript qs in questMap.Values)
        {
            //Debug.Log("QuestStateNotReached");
            if(qs.state == QuestState.REQUIREMENTS_NOT_MET && checkReqsMet(qs))
            {
                Debug.Log("QUest state reachd");
                changeQuestState(qs.info.id, QuestState.CAN_START);
            }
        }
    }

    private void changeQuestState(string id, QuestState state)
    {
        QuestScript quest = getQuestById(id);
        quest.state = state;
        GameManager.instance.gameEvents.questStateChange(quest); //tells questgiver that the quest has changed state
    }

    //changes state of indiv steps
    private void questStepStateChange(string id, int stepIndex, QuestStepState qsState )
    {
        QuestScript quest = getQuestById(id);
        quest.storeQuestStepState(qsState, stepIndex);
        changeQuestState(id, quest.state);
    }
    private void startQuest(string id)
    {
        Debug.Log("Start quest: " + id);
        QuestScript quest = getQuestById(id);
        quest.InstantiateCurrQuestStep(this.transform); //make the step in the quest managaer
        changeQuestState(id, QuestState.IN_PROGRESS);
    }

    private void advanceQuest(string id)
    {
        Debug.Log("Advance quest: " + id);
        QuestScript quest = getQuestById(id);
        quest.MoveToNextStep();

        if (quest.currStepExists())
        {
            quest.InstantiateCurrQuestStep(this.transform);
        }
        else
        {
            changeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void finishQuest(string id)
    {
        Debug.Log("Finsish quest: " + id);
        QuestScript quest = getQuestById(id);

        claimRewards(quest);
        changeQuestState(quest.info.id, QuestState.FINISHED);
    }

    //give the rewards to the player
    private void claimRewards(QuestScript quest)
    {
        //give them money here
        GameManager.instance.gold += quest.info.goldReward;
        GameManager.instance.exp += quest.info.expReward;
        //GameManager.instance.updateGold();
        GameManager.instance.gameEvents.collectExp(quest.info.expReward);
    }
    //check if you can do a quest
    private bool checkReqsMet(QuestScript quest) 
    {
        bool meetsReqs = true;

        if(currPlayerLevel < quest.info.playerLevelReq)
        {
            meetsReqs = false;
        }

        foreach (QuestSO qs in quest.info.questPrereqs)
        {
            if(getQuestById(qs.id).state != QuestState.FINISHED)
            {
                meetsReqs = false;
            } 
        }
        return meetsReqs;

    }

    private Dictionary<string, QuestScript> CreateQuestMap()
    {
        //this links to the resources folder im pretty sure
        QuestSO[] allQuests = Resources.LoadAll<QuestSO>("Quests");
        //create quest map
        Dictionary<string, QuestScript> idToQuestMap = new Dictionary<string, QuestScript>();

        foreach(QuestSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("DUPLICATE ID FOUND WHEN CREATING QUEST MAP!! ID: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new QuestScript(questInfo));
        }

        return idToQuestMap;
    }

    //accessing quest ids
    private QuestScript getQuestById(string id)
    {
        QuestScript quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("No id for quest in map: " + id);

        }
        return quest;
    }
}
