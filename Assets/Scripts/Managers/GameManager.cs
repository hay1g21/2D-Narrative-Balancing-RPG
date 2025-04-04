using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance; //static allows access from anywhere in code, even from other scripts
    public string sceneType; //ref to scene type

    public string gameState = "BEGIN";

    public bool cutScenePlaying;

    public bool firstBattle; //for showing tutorial

    public GameObject enemyData;

    public GameObject winScreen;

    public Dictionary<string, float> pStats;

    public int prevScene;

    //stats for levelling up
    public int healthGain;
    public int magicGain;
    public int meleegain;
    public int rangeGain;
    public float defenseGain;
    public float speedgain;

    public int highestLevel = 1;
    public int balanceLevel;

    public static string BEGIN_STATE = "BEGIN";
    public static string OVERWORLD_STATE = "OVERWORLD";
    public static string VICTORY_STATE = "VICTORY";
    public static string IN_PROGRESS_STATE = "IN_PROGRESS";
    public static string LOSE_STATE = "LOSE";

    //####-GAME EVENTS -####//

    public GameEvents gameEvents;

    private float itemChance = 75; //rng val for getting items


    //recording when items are collected
    public List<string> enemiesdefeated = new List<string>();
    public List<string> cutscenesPlayed = new List<string>();
    public List<string> keyDoorsOpened = new List<string>();
    public List<string> dialogueBoxes = new List<string>();
    // public List<string> itemsCollected = new List<string>();

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


        //initialise events
        gameEvents = new GameEvents();

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

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call a function or perform an action when the Escape key is pressed
            gameObject.transform.SetParent(GameObject.Find("Player").transform);
            GameObject.Find("QuestManager").transform.SetParent(GameObject.Find("Player").transform);
            GameObject.Find("InventoryCanvas").transform.SetParent(GameObject.Find("Player").transform);
            SceneManager.sceneLoaded -= onSceneLoaded; //runs every scene
            SceneManager.LoadScene(0);

            // For example, you can quit the application
            // Application.Quit();
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

        //delete items collected
        GameObject itemFolder = GameObject.Find("ItemFolder");
        //Debug.Log(itemFolder);
        if (InventoryManager.instance.itemIds != null)
           
        {
            foreach (Transform item in itemFolder.transform)
            {
                if (InventoryManager.instance.itemIds.Contains(item.gameObject.GetComponent<Item>().id))
                {
                    Destroy(item.gameObject);
                }
            }
        }

        //delete cutscene triggers
        GameObject cutSceneFolder = GameObject.Find("CutsceneFolder");
        //Debug.Log(itemFolder);

        foreach (Transform cutscene in cutSceneFolder.transform)
        {
            if (cutscenesPlayed.Contains(cutscene.gameObject.GetComponent<CutScenePlayer>().id.ToString()))
            {
                Destroy(cutscene.gameObject);
            }
        }

        GameObject keydoorFolder = GameObject.Find("KeyDoorFolder");

        foreach (Transform door in keydoorFolder.transform)
        {
            if (keyDoorsOpened.Contains(door.gameObject.GetComponent<Keydoor>().id.ToString()))
            {
                Destroy(door.gameObject);
            }
        }

        //delete dialogue boxes when used
        GameObject diagFolder = GameObject.Find("DialogueBoxes");

        foreach (Transform box in diagFolder.transform)
        {
            if (dialogueBoxes.Contains(box.gameObject.GetComponent<DiagPrompt>().id.ToString()))
            {
                Destroy(box.gameObject);
            }
        }


        //delete cutscene triggers



        player = GameObject.Find("Player");
        if (lastPos != null && gameState.Equals(VICTORY_STATE))
        {
            //Debug.Log("WHAT ARE YOU SAYING");
            
            //move player back to where they were
            player.transform.position = lastPos;
            if(pStats != null)
            {
                player.GetComponent<OverworldPlayer>().editVal("Health", pStats["Health"]);
                player.GetComponent<OverworldPlayer>().editVal("Magic", pStats["Magic"]);
                player.GetComponent<OverworldPlayer>().editVal("MaxHealth", pStats["MaxHealth"]);
                player.GetComponent<OverworldPlayer>().editVal("MaxMagic", pStats["MaxMagic"]);
                player.GetComponent<OverworldPlayer>().editVal("Melee", pStats["Melee"]);
                player.GetComponent<OverworldPlayer>().editVal("MagicRange", pStats["MagicRange"]);
            }

           
        //starting from screen e.g debugging
        }else if(gameState.Equals(LOSE_STATE)){
            player.transform.position = GameObject.Find("SpawnPoints/Start").transform.position;
            if (pStats != null)
            {
                player.GetComponent<OverworldPlayer>().editVal("Health", 1);
                player.GetComponent<OverworldPlayer>().editVal("Magic", pStats["Magic"]);
            }
        }
        else if (gameState.Equals("BEGIN"))
        {
            //Debug.Log("OI");
            player.transform.position = GameObject.Find("SpawnPoints/Start").transform.position;

        //travelling between states
        }else if (gameState.Equals(OVERWORLD_STATE))
        {
            //find previous gate and match to spawn
            Debug.Log("Prev scene: " + prevScene);
            string search = "SpawnPoints/" + prevScene;

            Transform thing = GameObject.Find(search).transform;
            player.transform.position = thing.position;
            if (pStats != null)
            {
                //carry over stats
                player.GetComponent<OverworldPlayer>().editVal("Health", pStats["Health"]);
                player.GetComponent<OverworldPlayer>().editVal("Magic", pStats["Magic"]);
                player.GetComponent<OverworldPlayer>().editVal("MaxHealth", pStats["MaxHealth"]);
                player.GetComponent<OverworldPlayer>().editVal("MaxMagic", pStats["MaxMagic"]);
                player.GetComponent<OverworldPlayer>().editVal("Melee", pStats["Melee"]);
                player.GetComponent<OverworldPlayer>().editVal("MagicRange", pStats["MagicRange"]);
            }
        }
        pStats = player.GetComponent<OverworldPlayer>().playerStats;

        //update balance slider
        GameObject sliderCanv = GameObject.Find("SliderCanvas");
        updateGold();
        //unneeded
        //updateExp();

        updateMagic();
        updateHealth();
        sliderCanv.GetComponent<SliderScript>().setSlider(balanceLevel);
        //update quest pointers again cause they're dumb
        QuestManager.instance.UpdateQuestPoints();
        
        gameState = OVERWORLD_STATE;

    }

    

    public void loadCombat()
    {
        //move player
        player = GameObject.Find("Player");
        player.transform.position = GameObject.Find("PlayerPos").transform.position;

        //add player data
        if (pStats != null)
        {
            FighterStats fStat = player.GetComponent<FighterStats>();
            fStat.setStats(pStats["Health"], pStats["Magic"], pStats["Melee"], pStats["MagicRange"], pStats["Defense"], pStats["Speed"],pStats["MaxHealth"], pStats["MaxMagic"]);
          

        }
        player.GetComponent<FighterStats>().setName("Bones");
        enemyData = GameObject.Find("Data");
        //enemyData.transform.position = new Vector3(50,50,enemyData.transform.position.z);
        Debug.Log("Load Combat");
        //get the enemy info and create the...enemy
        GameObject template = GameObject.Find("Enemies/Enemy");
        
        GameObject newEnemy = Instantiate(template);
        newEnemy.name = "CurrentEnemy"; //so gamecontroller script can refer to it
        //update its stats
        OverworldEnemy ed = enemyData.GetComponent<OverworldEnemy>();
        newEnemy.GetComponent<FighterStats>().setName(ed.enemyName);
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
        gameState = IN_PROGRESS_STATE; //set to battle in prog
        GameObject.Find("GameControllerObject").GetComponent<GameController>().StartBattle();



    }
    //return to overworld with 1 health and nearly dead, no rewards
    public void loseCombat()
    {
        Debug.Log("Battle Lost");
        gameState = LOSE_STATE;
        GameObject player = GameObject.Find("Player");
        pStats["Health"] = player.GetComponent<FighterStats>().health;
        pStats["Magic"] = player.GetComponent<FighterStats>().magic;
        winScreen.SetActive(true);
    }
    public void winCombat()
    {
        //quest signal
        gameEvents.enemyKilled(enemyData.GetComponent<OverworldEnemy>().enemyName);

        Debug.Log("Battle won");
        gameState = "VICTORY";
        //add enemy to be dead
        enemiesdefeated.Add(enemyData.GetComponent<OverworldEnemy>().getId());
        gold = gold +enemyData.GetComponent<OverworldEnemy>().getGold();
        exp = exp + enemyData.GetComponent<OverworldEnemy>().getExp();

        
        GameObject itemFolder = GameObject.Find("ItemFolder");
        //choose items to award with rng
        

        //item drop chance stats 4 now
        //1 item, 75%
        //2 quantity 20%
        //3 quantity 5%
        string[] itemsToWin = { "PoisonBolus", "Potion", "ManaPotion" };

        string itemRewards = "";

        
        Item item = null;
        float num = Random.Range(0, 101);
        Debug.Log("Get item chance" + num);

        int limit = 5;
        int limitcount = 0;
        if (num >= itemChance)
        {
            while(num >= itemChance && limitcount < limit)
            {
                float itemToPick = Random.Range(0, itemsToWin.Length);
                foreach (Transform itemObj in itemFolder.transform)
                {
                    if (itemObj.gameObject.GetComponent<Item>().getName().Equals(itemsToWin[(int)itemToPick]))
                    {
                        item = itemObj.GetComponent<Item>();
                    }
                }

                float quantityChance = Random.Range(0, 101);
                int quantity = 1;
                Debug.Log(quantityChance);
                if (quantityChance <= 75)
                {
                    quantity = 1;
                }
                else if (quantityChance > 75 && quantityChance <= 95)
                {
                    quantity = 2;
                }
                else if (quantityChance > 95)
                {
                    quantity = 3;
                }

                int extraItems = InventoryManager.instance.AddItem(item.getName(), quantity, item.getSprite(), item.getItemDesc());
                itemRewards = itemRewards + " " + item.getName() + ": " + quantity + ",";
                limitcount++;
                num = Random.Range(0, 101);
            }
            itemRewards = itemRewards.Substring(1, itemRewards.Length - 2);
            winScreen.GetComponent<WinScreen>().setItemReward(itemRewards);
        }
        else
        {
            winScreen.GetComponent<WinScreen>().setItemReward("No Items");
        }
        
        
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
        prevScene = SceneManager.GetActiveScene().buildIndex;
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
        //data.GetComponent<OverworldEnemy>().shouldChase = false;
        Vector3 Epos = new Vector3(50, 50, data.transform.position.z);
        enemyData = Instantiate(data, Epos, Quaternion.identity );
        enemyData.name = "Data";
        DontDestroyOnLoad(enemyData);
        
    }

    //called when the game levels up
    public void levelUp(int level)
    {
        //code for levelling up
        //edit pstats
        int diff = level - highestLevel;
        player = GameObject.Find("Player");
        pStats = player.GetComponent<OverworldPlayer>().playerStats;
        if (pStats != null)
        {
            float amount = pStats["MaxHealth"] + Mathf.Ceil(healthGain * diff);
            Debug.Log(amount);
            pStats["MaxHealth"] = amount;

            float amount2 = pStats["MaxMagic"] + Mathf.Ceil(magicGain * diff);
            pStats["MaxMagic"] = amount2;

            float damageAmount = meleegain;

            pStats["Melee"] = pStats["Melee"] + damageAmount;
            pStats["MagicRange"] = pStats["MagicRange"] + rangeGain;


        
        }
        updateHealth();
        updateMagic();
        Debug.Log("PLayer has levelled up");
    }

    //for overworld only atm
    public void changeHealth(int amount)
    {
        //change player stat and pstat then update text
        //if over the max just change
        /*
        if((pStats["Health"] + amount) >= pStats["MaxHealth"])
        {
            amount = (int)pStats["MaxHealth"] - (int)pStats["Health"];
            Debug.Log("Adding" + amount);
        }

        //if in combat change it differnetly
        if (gameState.Equals(IN_PROGRESS_STATE))
        {
            //pStats["Health"] = player.GetComponent<FighterStats>().health;
            player.GetComponent<FighterStats>().health = player.GetComponent<FighterStats>().health + amount;
            Debug.Log("New health = " + player.GetComponent<FighterStats>().health);
            pStats["Health"] = player.GetComponent<FighterStats>().health;
            Debug.Log("New health = " + pStats["Health"]);
        }
        else
        {
            player.GetComponent<OverworldPlayer>().editVal("Health", pStats["Health"] + amount);
        }
        */

        //slightly more complex if in game
        if (gameState.Equals(IN_PROGRESS_STATE))
        {

            FighterStats fs = player.GetComponent<FighterStats>();
            if ((fs.health + amount) >= pStats["MaxHealth"])
            {
                amount = (int)pStats["MaxHealth"] - (int)fs.health;
                Debug.Log("Adding" + amount);
            }
            //pStats["Health"] = player.GetComponent<FighterStats>().health;
            fs.health = fs.health + amount;
            pStats["Health"] = fs.health;
            Debug.Log("New magic level = " + fs.health);
        }
        else
        {
            if ((pStats["Health"] + amount) >= pStats["MaxHealth"])
            {
                amount = (int)pStats["MaxHealth"] - (int)pStats["Health"];
                Debug.Log("Adding" + amount);
            }
           

            player.GetComponent<OverworldPlayer>().editVal("Health", pStats["Health"] + amount);
        }


        //pStats["Health"] += amount;

        updateHealth();
        
    }

    public void pechToolTips()
    {
        if (gameState.Equals(IN_PROGRESS_STATE)){
            //access goblin database
            //set dialogue
            //say scene
            

            GameObject monsterMan = GameObject.Find("MonsterManager");
            monsterMan.GetComponent<MonsterManager>().getDescription();
        }
    }

    public void changeMagic(int amount)
    {
        //change player stat and pstat then update text
      
        //if over the max just change
        

        if (gameState.Equals(IN_PROGRESS_STATE))
        {
            
            FighterStats fs = player.GetComponent<FighterStats>();
            if ((fs.magic + amount) >= pStats["MaxMagic"])
            {
                amount = (int)pStats["MaxMagic"] - (int)fs.magic;
                Debug.Log("Adding" + amount);
            }
            //pStats["Health"] = player.GetComponent<FighterStats>().health;
            fs.magic =fs.magic+amount;
            pStats["Magic"] = fs.magic;
            Debug.Log("New magic level = " + fs.magic);
        }
        else
        {
            if ((pStats["Magic"] + amount) >= pStats["MaxMagic"])
            {
                amount = (int)pStats["MaxMagic"] - (int)pStats["Magic"];
                Debug.Log("Adding" + amount);
            }
            if (player.GetComponent<OverworldPlayer>() == null)
            {
                Debug.Log("Still in winscreen");
            }
            else
            {
                player.GetComponent<OverworldPlayer>().editVal("Magic", pStats["Magic"] + amount);
            }
           
        }
       
        //pStats["Health"] += amount;
        updateMagic();
        Debug.Log("Work");

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

    public void updateExp(int nextLevel)
    {
        Text expCounter = GameObject.Find("HeadsUpCanvas/MainPanel/ExpLabel/Count").GetComponent<Text>();
        expCounter.text = exp + "/" + nextLevel;

    }

    public void updateHealth()
    {
        //if in combat do it differently
        if (gameState.Equals(IN_PROGRESS_STATE))
        {
            Text healthCounter = GameObject.Find("HeadsUpCanvas/HeroInfo/HealthLabel").GetComponent<Text>();
            healthCounter.text = pStats["Health"] + "/" + pStats["MaxHealth"];
            FighterStats fs = player.GetComponent<FighterStats>();
            fs.updateHealthFill();
        }
        else
        {
            Text healthCounter = GameObject.Find("HeadsUpCanvas/MainPanel/HealthLabel/Count").GetComponent<Text>();
            healthCounter.text = pStats["Health"] + "/" + pStats["MaxHealth"];
        }
        

    }

    public void updateMagic()
    {
        //if in battle
        if (gameState.Equals(IN_PROGRESS_STATE))
        {
            Text magicCounter = GameObject.Find("HeadsUpCanvas/HeroInfo/MagicLabel").GetComponent<Text>();
            magicCounter.text = pStats["Magic"] + "/" + pStats["MaxMagic"];
            FighterStats fs = player.GetComponent<FighterStats>();
            fs.updateMagicFill();
        }
        else
        {
            if (GameObject.Find("HeadsUpCanvas/MainPanel/MagicLabel/Count")== null)
            {
                Debug.Log("Still in winscreen");
            }else
            {
                Text magicCounter = GameObject.Find("HeadsUpCanvas/MainPanel/MagicLabel/Count").GetComponent<Text>();
                magicCounter.text = pStats["Magic"] + "/" + pStats["MaxMagic"];
            }
            
        }
    }



}
