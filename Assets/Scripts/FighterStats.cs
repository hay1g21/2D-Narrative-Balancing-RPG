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
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damage)
    {
        health = health - damage;
        //animator.Play("Damage");

        //Set damage text

        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        }
        else if (damage > 0)
        {
            Debug.Log("Health calc: scale" + healthScale.x + " times " + health + "/" + startHealth);
            xNewHealthScale = healthScale.x * (health / startHealth); //updates ratio of health bar
            Debug.Log("Health scale now " + xNewHealthScale);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);        
        }
        //show damage text on screen
       
        GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
        GameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString();
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
        //if free
        if(cost > 1)
        {
            magic -= cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }
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
}
