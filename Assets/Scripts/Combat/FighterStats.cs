using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;

    public Text healthLabel;
    public Text magicLabel;

    [Header("Stats")]
    public float health;
    public float magic;
    public float melee;
    public float magicRange;
    public float defense;
    
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    public bool dead = false;
    //Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

   

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject GameControllerObj;

    // Start is called before the first frame update
    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        GameControllerObj = GameObject.Find("GameControllerObject");
        //update vals
        updateHealthCount();
        updateMagicCount();

}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStats(float health, float magic, float melee, float magicRange, float defense, float speed, float experience)
    {
        //set vars
        this.health = health;
        this.magic = magic;
        this.melee = melee;
        this.magicRange = magicRange;
        this.defense = defense;
        this.speed = speed;
        this.experience = experience;

        startHealth = health;
        startMagic = magic;
        //reset vals

        updateHealthCount();
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;


    }

    public void setStats(float health, float magic, float melee, float magicRange, float defense, float speed, float maxHealth,float maxMagic)
    {
        //set vars
        this.health = health;
        this.magic = magic;
        this.melee = melee;
        this.magicRange = magicRange;
        this.defense = defense;
        this.speed = speed;

        startHealth = maxHealth;
        startMagic = maxMagic;
        //reset vals

        updateHealthCount();
        updateMagicCount();
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        xNewHealthScale = healthScale.x * (health / startHealth); //updates ratio of health bar
                                                                  //Debug.Log("Health scale now " + xNewHealthScale);
        healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);

        xNewMagicScale = magicScale.x * (magic / startMagic);
        magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
    }

    public void ReceiveDamage(float damage)
    {
        health = health - damage;
        //animator.Play("Damage");

        
        if(health <= 0)
        {
            dead = true;
            //gameObject.tag = "Dead";
            String tag = gameObject.tag;
            health = 0;
            updateHealthCount();
            GameControllerObj.GetComponent<GameController>().cleanUP();
            Destroy(healthFill);
            Destroy(gameObject);
            if (tag.Equals("Enemy"))
            {
                GameManager.instance.winCombat();
            }
            else
            {
                //lose combat
                Debug.Log("You lost");
            }
           
        }
        else if (damage > 0)
        {
            //Debug.Log("Health calc: scale" + healthScale.x + " times " + health + "/" + startHealth);
            xNewHealthScale = healthScale.x * (health / startHealth); //updates ratio of health bar
            //Debug.Log("Health scale now " + xNewHealthScale);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
            updateHealthCount();
        }
        //show damage text on screen
       if(health != 0)
        {
            GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
            if (tag.Equals("Player"))
            {
                GameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString() + " Damage Taken";
            }else if (tag.Equals("Enemy"))
            {
                GameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString() + " Damage Dealt";
            }
            
        }
       
        if (damage <= 0)
        {
            GameControllerObj.GetComponent<GameController>().battleText.text = "BLOCKED";
        }
        //make next turn after 2 seconds
        Invoke("ContinueGame", 2);
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed); //rounds up to nearest int
    }

    public void updateMagicFill(float cost)
    {
        //if not free
        if(cost > 1)
        {
            magic -= cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
            updateMagicCount();

        }
    }

    //for game manager use
    public void updateMagicFill()
    {
        xNewMagicScale = magicScale.x * (magic / startMagic);
        magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        //updateMagicCount();
    }

    public void updateHealthFill()
    {
        xNewHealthScale = healthScale.x * (health / startHealth); //updates ratio of health bar
                                                                  //Debug.Log("Health scale now " + xNewHealthScale);
        healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        //updateMagicCount();
    }

    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }

    public int CompareTo(object otherstat)
    {
        //compares one fighter stat to another to choose turn based on speed
        int nex = nextActTurn.CompareTo(((FighterStats)otherstat).nextActTurn);
        return nex;
    }
    //returns dead
    public bool GetDead()
    {
        return dead;
    }

    public void updateHealthCount()
    {
        if(healthLabel != null)
        {
            healthLabel.text = health + "/" + startHealth;
        }
    }
    public void updateMagicCount()
    {
        if(magicLabel != null)
        {
            //change text
            magicLabel.text = magic + "/" + startMagic;
        }
        
    }
}
