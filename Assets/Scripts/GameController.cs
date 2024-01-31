using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // Start is called before the first frame update

    private List<FighterStats> fighterStats; //list of fighters

    [SerializeField]
    private GameObject battleMenu; //option menu to make player attack

    private bool canAttack; //

    public Text battleText;
    void Start()
    {
        fighterStats = new List<FighterStats>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        FighterStats currentFighterStats = player.GetComponent<FighterStats>();
        currentFighterStats.CalculateNextTurn(0); //fighterstats calcs turn with speed
        fighterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
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
            }
            else
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "magic";
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
