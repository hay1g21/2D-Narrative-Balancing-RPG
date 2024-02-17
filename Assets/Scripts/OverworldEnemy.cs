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
  

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
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
