using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject magicPrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;

    public GameObject minigame;


    public void setAttacks(GameObject a1, GameObject a2)
    {
        meleePrefab = a1;
        magicPrefab = a2;
    }
    public void SelectAttack(string btn)
    {
        GameObject victim = player;
        if (tag == "Player")
        {
            victim = enemy;
        }
        if (btn.Equals("melee"))
        {
            //call an attack
            if(tag == "Player")
            {
                //Debug.Log("DO SOMETHING");
                minigame.GetComponent<CombatMinigame>().finished = false;
                StartCoroutine(playMinigame(victim));
               
                
            }
            else
            {
                meleePrefab.GetComponent<AttackScript>().Attack(victim);
                Debug.Log("Melee");
            }
            
        }
        else if (btn.Equals("magic"))
        {
            Debug.Log("Magic");
            magicPrefab.GetComponent<AttackScript>().Attack(victim);
  
        }
        else
        {
            //meleeAttack.GetComponent<AttackScript>().Attack(victim)
            Debug.Log("Res");
            GameObject.Find("GameControllerObject").GetComponent<GameController>().switchAttack();
        }
    }
    void Start()
    {
        //get player and enemt
        Debug.Log("bruh");
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.Find("CurrentEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playMinigame(GameObject victim)
    {
        Debug.Log("DO SOMETHIING");
        minigame.GetComponent<CombatMinigame>().startMinigame();
        while (!minigame.GetComponent<CombatMinigame>().finished)
        {
            
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("Hello");
        if (minigame.GetComponent<CombatMinigame>().getBonus)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim, 1.2f);
        }
        else
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
        }
        
        yield return null;
    }

}
