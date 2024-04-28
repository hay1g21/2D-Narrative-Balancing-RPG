using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //Code is modified from https://github.com/kurtkaiser/RPG-Battle-Game-Video/blob/master/Assets/Scripts/GameController.cs by kurtkaiser (2020)

    // Start is called before the first frame update

    public List<FighterStats> fighterStats; //list of fighters
    public static GameController instance; //static allows access from anywhere in code, even from other scripts

    public GameObject damageEffect; //dmg effect
    

    public void OnEnable()
    {
        //subscribe to events
        GameManager.instance.gameEvents.onItemUsed += useItem;
        
    }

    private void OnDisable()
    {
        //unsub
        GameManager.instance.gameEvents.onItemUsed-= useItem;
      
    }
    public void Awake()
    {
        if (GameController.instance != null)
        {
            //can destroy other objs here if they dupe
            Destroy(gameObject);
            return;
        }

        instance = this; //assigns itself to gamemanager object in the scene
      
    }

    //use item, change the fact you used it as a turn, deac menu. So on.
    public void useItem()
    {
        GameController.instance.switchAttack();
        Debug.Log("USE ITEM");
        InventoryManager.instance.forceClosed();
        
        Invoke("NextTurn", 2);
    }

    public void itemDamage(int damage)
    {
        //find enemy and damage it
        GameObject enemy = GameObject.Find("CurrentEnemy");
        enemy.GetComponent<FighterStats>().ReceiveDamage(damage);
    }

    [SerializeField]
    private GameObject battleMenu; //option menu to make player attack

    public bool canAttack; //

    public Text battleText;
    //starts the battle having loaded everything in
    public void StartBattle()
    {
        fighterStats = new List<FighterStats>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        FighterStats currentFighterStats = player.GetComponent<FighterStats>();
        currentFighterStats.CalculateNextTurn(0); //fighterstats calcs turn with speed
        fighterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.Find("CurrentEnemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort(); //sorting the list based on the speeds
        this.battleMenu.SetActive(false);
        //set so can attack
        canAttack = true;
        NextTurn(); //commanding turn
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //when next turn is not called because the enemy died

    public void playAnim(GameObject target)
    {
        //find target

        Vector3 random = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
        damageEffect.transform.position = target.transform.position + random;
        damageEffect.GetComponent<Animator>().SetTrigger("TakeDamage");

    }
    public void cleanUP()
    {
        Debug.Log("Do something mate");
        battleText.gameObject.SetActive(false);
    }
    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);
        FighterStats currentFighterStats = fighterStats[0]; //first person in list
        fighterStats.Remove(currentFighterStats);
        //if they are not dead...
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            //calculates a turn again
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            //adds back and sorts again
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();
            if(currentUnit.tag == "Player")
            {
                this.battleMenu.SetActive(true);
                canAttack = true;
                //inventory manager, allow menu to be openeded

            }
            else
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "magic";
                if (attackType.Equals("melee"))
                {
                    Debug.Log("Enemy chooses melee attack");
                }else if (attackType.Equals("magic"))
                {
                    Debug.Log("Enemy chooses magic attack");
                }
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        }
        else
        {
            NextTurn();
        }
    }
    public void switchAttack()
    {
        canAttack = !canAttack;
    }

    public bool getAttack()
    {
        return canAttack; 
    }
}
