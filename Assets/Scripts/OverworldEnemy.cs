using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{

    //script for storing ove enemy information

    public string enemyName;
    public string id;
    
    public int enemyHealth;
    public int magic;
    public int meleedmg;
    public int magicrange;
    public int def;
    public int speed;
    public int exp;
    public int gold;

    public GameObject meleePrefab;

    public GameObject magicPrefab;

    public SpriteRenderer spriteRen;

    //for chasing ai
    public GameObject player;
    public float chaseSpeed;
    public float distance; //how far before chase is triggered
    public float aggroRange;


    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //ai movement
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position; //find diff
            direction.Normalize();
           

            if(distance < aggroRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                //maybe make them go back to original spot
            }
        }
    }
        

    public string getName()
    {
        return enemyName;
    }

    public string getId()
    {
        return id;
    }

    public SpriteRenderer getSprite()
    {
        return spriteRen;
    }

    public GameObject getMelee()
    {
        return meleePrefab;
    }


    public GameObject getMagic()
    {
        return magicPrefab;
    }

    
    
    public int getEnemyHealth()
    {
        return enemyHealth;
    }

    public int getEnemyMagic()
    {
        return magic;
    }

    public int getEnemyMelee()
    {
        return meleedmg;
    }

    public int getEnemyRange()
    {
        return magicrange;
    }

    public int getEnemyDef()
    {
        return def;
    }

    public int getEnemySpeed()
    {
        return speed;
    }
    public int getExp()
    {
        return exp;
    }
    public int getGold()
    {
        return gold;
    }
}
