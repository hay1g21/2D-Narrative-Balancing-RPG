using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance; //static allows access from anywhere in code, even from other scripts
    public string sceneType; //ref to scene type

    public string battleState = "IN_PROGRESS";

    public GameObject enemyData;

    public GameObject winScreen;

    public Dictionary<string, float> pStats;

    public int prevScene;

    

    public List<string> enemiesdefeated = new List<string>();

    public void Awake()
    {
        if (GameManager.instance != null)
        {
            //can destroy other objs here if they dupe
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll(); //deletes data use for debug

        instance = this; //assigns itself to gamemanager object in the scene
        //sceneloaded is an event which fires from scenemanager when scene is loaded
        //everytime its fired thr += makes fire every func inside event (inthiscase loadstate and some others)
        //SceneManager.sceneLoaded += loadState; //runs once at start
        SceneManager.sceneLoaded += onSceneLoaded; //runs every scene
        DontDestroyOnLoad(gameObject);
        //tileMap.SetActive(false);
    }

    //holds info about the environment side

    public GameObject player;

    public Vector3 lastPos;

    public int gold = 0; //amount of money player owns
    public int exp = 0;

    public GameObject[] enemies; //list of enemies in the map
    public GameObject[] items; //list of enemies in the map
    public GameObject[] npcs; //list of enemies in the map

    public void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //setup environ
        Debug.Log("Loading Scene");
        if (sceneType.Equals("overworld")){
            loadOve();
        }
        else if(sceneType.Equals("combat")){
            loadCombat();        
        }
    }

    public void loadOve()
    {
        Debug.Log("Load Environment");

        //delete all enemies already defeated
        GameObject enemyfolder = GameObject.Find("Enemies");

        foreach(Transform enemy in enemyfolder.transform)
        {
            if (enemiesdefeated.Contains(enemy.gameObject.GetComponent<OverworldEnemy>().getId()))
            {
                Destroy(enemy.gameObject);
            }
        }

        if(lastPos != null)
        {
            player = GameObject.Find("Player");
            //move player back to where they were
            player.transform.position = lastPos;
            if(pStats != null)
            {
                player.GetComponent<OverworldPlayer>().editVal("Health", pStats["Health"]);
                player.GetComponent<OverworldPlayer>().editVal("Magic", pStats["Magic"]);
            }

            pStats = player.GetComponent<OverworldPlayer>().playerStats;

        }
        updateGold();
        updateExp();
        updateMagic();
        updateHealth();
    }

    public void loadCombat()
    {
        //move player
        GameObject player = GameObject.Find("Player");
        player.transform.position = GameObject.Find("PlayerPos").transform.position;

        //add player data
        if (pStats != null)
        {
            FighterStats fStat = player.GetComponent<FighterStats>();
            fStat.setStats(pStats["Health"], pStats["Magic"], pStats["Melee"], pStats["MagicRange"], pStats["Defense"], pStats["Speed"],pStats["MaxHealth"], pStats["MaxMagic"]);

        }
        enemyData = GameObject.Find("Data");
        Debug.Log("Load Combat");
        //get the enemy info and create the...enemy
        GameObject template = GameObject.Find("Enemies/Enemy");
        
        GameObject newEnemy = Instantiate(template);
        newEnemy.name = "CurrentEnemy"; //so gamecontroller script can refer to it
        //update its stats
        OverworldEnemy ed = enemyData.GetComponent<OverworldEnemy>();
        newEnemy.GetComponent<FighterStats>().setStats(ed.getEnemyHealth(),ed.getEnemyMagic(),ed.getEnemyMelee(),ed.getEnemyRange(),ed.getEnemyDef(),ed.getEnemySpeed(),ed.getExp());
        newEnemy.transform.position = new Vector3(1, 1, transform.position.z);
        //make pretty
        newEnemy.GetComponent<SpriteRenderer>().sprite = ed.getSprite().sprite;
        newEnemy.GetComponent<SpriteRenderer>().color = ed.getSprite().color;

        GameObject.Find("HeadsUpCanvas/EnemyInfo/EnemyName").GetComponent<Text>().text = ed.getName();
        //move
        Transform spawn = GameObject.Find("Enemy1Pos").transform;
        newEnemy.transform.position = spawn.transform.position;

        //enemy now in place so set its attacks
        //remove attacks
        foreach(Transform transform in newEnemy.transform)
    {
            Destroy(transform.gameObject);
        }
        //set them from data
        GameObject meAt = Instantiate(ed.getMelee());
        GameObject magAt = Instantiate(ed.getMagic());

        //setfighter stats or whatever
            
        meAt.name = "MAttack";
        magAt.name = "MagAttack";

        meAt.transform.parent = newEnemy.transform;
        magAt.transform.parent = newEnemy.transform;

        meAt.GetComponent<AttackScript>().setOwner();
        magAt.GetComponent<AttackScript>().setOwner();

        newEnemy.GetComponent<FighterAction>().setAttacks(meAt, magAt);

        winScreen = GameObject.Find("HeadsUpCanvas/WinPrompt");
        winScreen.SetActive(false);

        Debug.Log("Done loading mate");

        //now start the battle
        GameObject.Find("GameControllerObject").GetComponent<GameController>().StartBattle();



    }

    public void winCombat()
    {
        Debug.Log("Battle won");
        battleState = "VICTORY";
        //add enemy to be dead
        enemiesdefeated.Add(enemyData.GetComponent<OverworldEnemy>().getId());
        gold = gold +enemyData.GetComponent<OverworldEnemy>().getGold();
        exp = exp + enemyData.GetComponent<OverworldEnemy>().getExp();
        //toOverworld(prevScene);
        //move player
        GameObject player = GameObject.Find("Player");
        pStats["Health"] = player.GetComponent<FighterStats>().health;
        pStats["Magic"] = player.GetComponent<FighterStats>().magic;
        winScreen.SetActive(true);
        //winScreen.GetComponent<WinScreen>().setActive(false);
    }


    //loading screens
    public void toOverworld(int sceneNum)
    {
        sceneType = "overworld"; //so gamemanager knows what scene it is
        if(enemyData != null)
        {
            Destroy(enemyData);
        }
        
        SceneManager.LoadScene(sceneNum);
    }

    public void toCombat(GameObject data)
    {
        //save location of player
        player = GameObject.Find("Player");
        lastPos = player.transform.position;
        pStats = player.GetComponent<OverworldPlayer>().playerStats;
        //prepare things for moving to next scene
        sceneType = "combat";
        prevScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
        //enemyData = data;
        enemyData = Instantiate(data);
        enemyData.name = "Data";
        DontDestroyOnLoad(enemyData);
        
    }

    

    public int getGold()
    {
        return gold;
    }

    public int getExp()
    {
        return exp;
    }

    public void updateGold()
    {
        Text goldCounter = GameObject.Find("HeadsUpCanvas/MainPanel/MoneyLabel/Count").GetComponent<Text>();
        goldCounter.text = gold + "";

    }
    public void updateExp()
    {
        Text expCounter = GameObject.Find("HeadsUpCanvas/MainPanel/ExpLabel/Count").GetComponent<Text>();
        expCounter.text = exp + "";

    }

    public void updateHealth()
    {
        Text expCounter = GameObject.Find("HeadsUpCanvas/MainPanel/HealthLabel/Count").GetComponent<Text>();
        expCounter.text = pStats["Health"] + "/" + pStats["MaxHealth"];

    }

    public void updateMagic()
    {
        Text expCounter = GameObject.Find("HeadsUpCanvas/MainPanel/MagicLabel/Count").GetComponent<Text>();
        expCounter.text = pStats["Magic"] + "/" + pStats["MaxMagic"];

    }

}
