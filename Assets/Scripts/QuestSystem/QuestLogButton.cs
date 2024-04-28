using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

//Code is modified from https://github.com/shapedbyrainstudios/quest-system/blob/3-quest-log-implemented/Assets/Scripts/UI/QuestLogButton.cs by trevermock (2023)

public class QuestLogButton : MonoBehaviour, ISelectHandler
{

    public Button button { get; private set; }
    //button will be disabled, so cant use start or awake methods
    private TextMeshProUGUI buttText;
    private UnityAction onSelectAction;

    //set display name to the button text
    public void initialize(string displayName,UnityAction action)
    {
        this.button = this.GetComponent<Button>();
        this.buttText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.buttText.text = displayName;
        this.onSelectAction = action;
    }
    //fires when button is selected
    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }

    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
            case QuestState.CAN_START:
                buttText.color = Color.red;
                break;
            case QuestState.IN_PROGRESS:
                
            case QuestState.CAN_FINISH:
                buttText.color = Color.yellow;
                break;
            case QuestState.FINISHED:
                buttText.color = Color.green;
                break;
            default:
                Debug.LogWarning("Quest state not recognised");
                break;
        }
    }
}
