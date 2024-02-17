using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{

    //script for storing ove enemy information

    public string playername;
    public string id;



    /*
    public int playerHealth;
    public int magic;
    public int meleedmg;
    public int magicrange;
    public int def;
    public int speed;
    public int exp;
    public int gold;
    */

    public Dictionary<string, float> playerStats = new Dictionary<string, float>(){
        {"Health", 100 },
        {"MaxHealth",100 },
        {"Magic", 50 },
        {"MaxMagic",50 },
        {"Melee", 10 },
        {"MagicRange",10 },
        {"Defense", 1.2f },
        {"Speed", 10 },
    };

    //for editing the stats
    public void editVal(string key, float val)
    {

        playerStats[key] = val;

    }

    public float getVal(string key)
    {
        return playerStats[key];
    }






    public GameObject meleePrefab;

    public GameObject magicPrefab;

    public SpriteRenderer spriteRen;

    //put stats in here as well

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detects if touches an enemy
        if(collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.toCombat(collision.gameObject);



        }
        //detects if it touches a level gate
    }
}
