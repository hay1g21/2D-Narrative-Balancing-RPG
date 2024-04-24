using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string enemyName;
    public int enemyHealth;
    public int magic;
    public int meleedmg;
    public int magicrange;
    public int def;
    public int speed;
    public int exp;
    public int gold;

    private string[] speaker = {"Pech"};
    private string[] finalDiag;

    private string[] enemyList = { "Goblin", "Shaman", "GoblinGeneral", "Carnivlora" };
    [TextArea]
    public string[] descs; //for eahc enemy

    public DialogueChoiceSO[] allChoices; 

    public List<DialogueChoiceSO> currChoices;


    public string attackStrength;

    void Start()
    {
        OverworldEnemy monsterStats = GameObject.Find("Data").GetComponent<OverworldEnemy>();

        allChoices = Resources.LoadAll<DialogueChoiceSO>("ChoiceObjects"); //load in mult choices for combat minigames


        if (GameManager.instance.balanceLevel > 4)
        {
            enemyName = monsterStats.enemyName;
        }
        else
        {
            enemyName = monsterStats.enemyName;
            enemyHealth = monsterStats.enemyHealth;
            magic = monsterStats.magic;
            meleedmg = monsterStats.meleedmg;
            magicrange = monsterStats.magicrange;
            def = monsterStats.def;
            speed = monsterStats.speed;
            exp = monsterStats.exp;
            gold = monsterStats.gold;


        }

        string currEnemy = enemyName;

        Debug.Log(allChoices.Length);
        for (int i = 0; i < allChoices.Length; i++)
        {
            Debug.Log("Comparing Enemy" + allChoices[i].relatedEnemy + " with enemy: " + currEnemy);
            if (allChoices[i].relatedEnemy.Equals(currEnemy))
            {
                Debug.Log("Add");
                currChoices.Add(allChoices[i]);
            }
            if (currChoices.Count >= 3)
            {
                break;
            }
        }


    }

    public List<DialogueChoiceSO> getCurrChoices()
    {
        return currChoices;
    }

    public string getChoiceDesc(int choice)
    {
        return currChoices[choice].dialogueChoice;

    }

    public string getGoodChoice()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.good))
            {
                good = currChoices[i].dialogueChoice;
            }
        }
        return good;
    }

    public string getMedChoice()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.medium))
            {
                good = currChoices[i].dialogueChoice;
            }
        }
        return good;
    }

    public string getBadChoice()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.bad))
            {
                good = currChoices[i].dialogueChoice;
            }
        }
        return good;
    }

    public string getGoodAttack()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.good))
            {
                good = currChoices[i].attackDesc;
            }
        }
        return good;
    }

    public string getMedAttack()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.medium))
            {
                good = currChoices[i].attackDesc;
            }
        }
        return good;
    }

    public string getBadAttack()
    {
        string good = null;
        for (int i = 0; i < currChoices.Count; i++)
        {
            if (currChoices[i].optionQual.Equals(DialogueChoiceSO.OptionQuality.bad))
            {
                good = currChoices[i].attackDesc;
            }
        }
        return good;
    }

    public void setAttack(DialogueChoiceSO.OptionQuality qual)
    {
        attackStrength = qual.ToString();
    }
    public void getDescription()
    {
        
        //get these details first
        if (GameManager.instance.balanceLevel <= 1)
        {
            //put all the details into one string
            finalDiag = new string[] { "Name: " + enemyName + ", HP:" + enemyHealth + ", MP:" + magic + ", Dmg: " + meleedmg + ", Magic Dmg: " + magicrange + ", Defense: " + def + ", Speed: " + speed + ", Exp: " + exp + "Gold: " + gold };
           
        }
        else
        {
            if(GameManager.instance.balanceLevel > 1 && GameManager.instance.balanceLevel <= 5)
            {
                //fetch desc from the list
                for (int i = 0; i < enemyList.Length; i++)
                {
                    if (enemyList[i].Equals(enemyName))
                    {
                        finalDiag = new string[] { descs[i], "Name: " + enemyName + ", HP:" + enemyHealth + ", MP:" + magic + ", Dmg: " + meleedmg + ", Magic Dmg: " + magicrange + ", Defense: " + def + ", Speed: " + speed + ", Exp: " + exp + "Gold: " + gold };
                        speaker = new string[] {"Pech","Pech"};
                    }
                }
            }
            else
            {
                //on narrative level 6 & 7, have it just the exposition;
                for (int i = 0; i < enemyList.Length; i++)
                {
                    if (enemyList[i].Equals(enemyName))
                    {
                        finalDiag = new string[] { descs[i]};
                    }
                }
            }
           
            

           
        }
        DialogueManager.instance.setText(finalDiag, speaker, 0.03f);
        DialogueManager.instance.dialogueSequence();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //get the stats of a monster
}
