using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NarrativeMinigame : MonoBehaviour
{
    public GameObject panel;

   

    public bool getBonus;

    public DialogueChoiceSO[] allChoices;

    public List<DialogueChoiceSO> currChoices;

    public List<Button> buttons;

    public GameObject monsterManager;

    public bool finished;
    // Start is called before the first frame update
    void Start()
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);

        }
        allChoices = Resources.LoadAll<DialogueChoiceSO>("ChoiceObjects");

        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startMinigame()
    {
        Debug.Log("Start minigame");

        //currChoices = new List<DialogueChoiceSO>();

        currChoices = monsterManager.GetComponent<MonsterManager>().getCurrChoices();
        //show the panel, then
        getBonus = false;
        panel.SetActive(true);

        /*
        string currEnemy = GameObject.Find("MonsterManager").GetComponent<MonsterManager>().enemyName;

        Debug.Log(allChoices.Length);
        for (int i = 0; i < allChoices.Length; i++)
        {
            Debug.Log("Comparing Enemy" + allChoices[i].relatedEnemy + " with enemy: " + currEnemy);
            if (allChoices[i].relatedEnemy.Equals(currEnemy))
            {
                Debug.Log("Add");
                currChoices.Add(allChoices[i]);
            }
            if(currChoices.Count >= 3)
            {
                break;
            }
        }
        */

        foreach (Button butt in buttons)
        {
            TMP_Text buttonText = butt.GetComponentInChildren<TMP_Text>();
            Debug.Log(buttons.IndexOf(butt));
            Debug.Log(buttonText.text);
            buttonText.text = currChoices[buttons.IndexOf(butt)].dialogueChoice;
        }


    }

    public void butt1()
    {
        if (currChoices[0].optionQual.Equals(DialogueChoiceSO.OptionQuality.good))
        {
            Debug.Log("Right choice");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.good);
        }
        else if (currChoices[0].optionQual.Equals(DialogueChoiceSO.OptionQuality.medium))
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.medium);
        }
        else
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.bad);
        }
    }

    public void butt2()
    {
        if (currChoices[1].optionQual.Equals(DialogueChoiceSO.OptionQuality.good))
        {
            Debug.Log("Right choice");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.good);
        }
        else if (currChoices[1].optionQual.Equals(DialogueChoiceSO.OptionQuality.medium))
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.medium);
        }
        else
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.bad);
        }
    }

    public void butt3()
    {
        if (currChoices[2].optionQual.Equals(DialogueChoiceSO.OptionQuality.good))
        {
            Debug.Log("Right choice");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.good);
        }
        else if (currChoices[2].optionQual.Equals(DialogueChoiceSO.OptionQuality.medium))
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.medium);
        }
        else
        {
            Debug.Log("WRONG");
            getBonus = true;
            finished = true;
            panel.SetActive(false);
            monsterManager.GetComponent<MonsterManager>().setAttack(DialogueChoiceSO.OptionQuality.bad);
        }
    }





}
