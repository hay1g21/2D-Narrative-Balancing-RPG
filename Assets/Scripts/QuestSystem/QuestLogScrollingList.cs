using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class QuestLogScrollingList : MonoBehaviour
{
    [SerializeField] //need ref to where to instantiate#
    private GameObject contentParent;

    [SerializeField]
    private GameObject questLogButtonPrefab;

    //stop dupes with dict
    private Dictionary<string, QuestLogButton> idToButtMap = new Dictionary<string, QuestLogButton>();

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;
  
    private void Start()
    {
        /*
        for(int i = 0; i<20; i++)
        {
            QuestSO questInftest = ScriptableObject.CreateInstance<QuestSO>();
            questInftest.id = "test_" + i;
            questInftest.displayName = "Test " + i;
            questInftest.questSteps = new GameObject[0];
            QuestScript quest = new QuestScript(questInftest);

            QuestLogButton buttn = createButtIfNotExist(quest, () => {
                Debug.Log("Selected: " + questInftest.displayName);
                });
            if (i == 0)
            {
                buttn.button.Select();
            }

        }
        */

    }
    public QuestLogButton createButtIfNotExist(QuestScript quest, UnityAction selectAction)
    {
        QuestLogButton butt = null;
        //check if it doesnt already contain a key
        if (!idToButtMap.ContainsKey(quest.info.id))
        {
            butt = instantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            //return instead of instantiating
            butt = idToButtMap[quest.info.id];
        }
        return butt;
    }
    //passes along quest and a select action to the button & creates button
    private QuestLogButton instantiateQuestLogButton(QuestScript quest, UnityAction selectAction)
    {
        //create a button
        QuestLogButton butt = Instantiate(questLogButtonPrefab, contentParent.transform).GetComponent<QuestLogButton>();
        //name the button to be the quest id
        butt.gameObject.name = quest.info.id + "_button";
        RectTransform buttRectTransform = butt.GetComponent<RectTransform>();
        //pass along to initialise when button is selected
        butt.initialize(quest.info.displayName, () =>
        {
            selectAction();
            UpdateScrolling(buttRectTransform);
        });
        idToButtMap[quest.info.id] = butt;
        return butt;
    }

    //caleld when a button is selected

    private void UpdateScrolling(RectTransform buttRectTrans)
    {
        //tracks top and button pos of each button
        float buttonYMin = Mathf.Abs(buttRectTrans.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttRectTrans.rect.height;

        //calc two others - for height of scroll view, shifts to add invis buttons
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;
        // Shift!
        if(buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                    contentRectTransform.anchoredPosition.x,
                    buttonYMax - scrollRectTransform.rect.height
                );
        }
        else if(buttonYMin < contentYMin)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                    contentRectTransform.anchoredPosition.x,
                    buttonYMin
                );
        }
    }
}
