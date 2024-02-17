using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public GameObject owner; //owner of gameobject

    [SerializeField]
    private bool magicAttack; //check to be magic, costs magic point if checked

    [SerializeField]
    private float magicCost; //cost of spell

    [SerializeField]
    private string animationName;

    [SerializeField]
    private float minAttackMult; //range of vals attack can be
    [SerializeField]
    private float maxAttackMult;
    [SerializeField]
    private float minDefenseMult;
    [SerializeField]
    private float maxDefenseMult;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
       // magicScale = GameObject.Find("HeroMagicFill").GetComponent<RectTransform>().localScale;
    }

    //for setup
    public void setOwner()
    {
        owner = this.gameObject.transform.parent.gameObject;
    }
    public void Attack(GameObject victim)
    {
        //get stats of both enemies
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();
        Debug.Log(victim.name + " is being attacked");
       
        if(attackerStats.magic >= magicCost) //if they have sufficient magic points allow spell - Melee costs 0 so is all good
        {
            float multiplier = Random.Range(minAttackMult, maxAttackMult);
        
            damage = multiplier * attackerStats.melee;
         
            if(magicAttack)
            {
                damage = multiplier * attackerStats.magicRange;
                //attackerStats.magic -= magicCost;
            }
            float defenseMultiplier = Random.Range(minDefenseMult, maxDefenseMult);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
            Debug.Log("Damage done to target: " + damage);
            //owner.GetComponent<Animator>().Play(animationName); for animation
            attackerStats.updateMagicFill(magicCost);
            targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
        }else if (owner.tag.Equals("Enemy"))
        {
            //Enemy done goofed and did magic attack
            Debug.Log("Enemy ran out of magic lol magic left = " + attackerStats.magic);
            Invoke("SkipTurnContinueGame", 2);
        }else if (owner.tag.Equals("Player"))
        {
            //reset choice
            GameObject.Find("GameControllerObject").GetComponent<GameController>().switchAttack();
        }
        else
        {
            
        }
       
    }

    void SkipTurnContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
