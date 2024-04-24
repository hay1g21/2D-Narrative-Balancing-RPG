using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpSystem : MonoBehaviour
{

    //starts at 0
    public int playerLevel;
    /*
    {"Health", 100 },
        { "MaxHealth",100 },
        { "Magic", 50 },
        { "MaxMagic",50 },
        { "Melee", 10 },
        { "MagicRange",10 },
        { "Defense", 1.2f },
        { "Speed", 10 },
    */

    private float xpStep = 12;
    public float xpStepMult;
    private int firstStep = 20;
    //.if in overworld, update exp counter
    //if limit overcome, increment level, call gamemanager to do stuff

    public int[] xpLevels = { };
    

    public int currExp;
    public int currIndex;

    public Text playerText;
    // Start is called before the first frame update
    public void OnEnable()
    {
        //subscribe to events
        GameManager.instance.gameEvents.onExpCollected += expCollected;

    }

    private void OnDisable()
    {
        //unsub
        GameManager.instance.gameEvents.onExpCollected -= expCollected;

    }
    private void Awake()
    {
        //create level tree
        xpLevels = new int[20];
        xpLevels[0] = firstStep;
        for (int i = 1; i < xpLevels.Length; i++)
        {
            xpLevels[i] = xpLevels[i - 1] + (int)Mathf.Ceil(xpStep * xpStepMult);
        }
    }
    public void Start()
    {
        currExp = GameManager.instance.exp;
        playerText.text = "Level: " + playerLevel.ToString();
        updateLevels();
    }
    
    public void updateLevels()
    {
        currIndex = 0;
        foreach (int levelCount in xpLevels)
        {
            //print("Checking level count " + levelCount + " with current exp " + xpLevels);
            if (levelCount <= currExp)
            {

                playerLevel++;
                Debug.Log("Reached next level: " + playerLevel);
                currIndex++;

            }
        }

        GameManager.instance.updateExp(xpLevels[currIndex]);
        if(GameManager.instance.highestLevel < playerLevel)
        {
            GameManager.instance.levelUp(playerLevel);
            GameManager.instance.highestLevel = playerLevel;
        }
        playerText.text = "Level: " + playerLevel.ToString();
    }

    public void expCollected(int amount)
    {
        Debug.Log("Collected " + amount + "xp");

        currExp = currExp + amount;
        playerLevel = 1;
        //now check if its passed current level
        currIndex = 0;
        foreach(int levelCount in xpLevels)
        {
            //print("Checking level count " + levelCount + " with current exp " + xpLevels);
            if (levelCount <= currExp)
            {
               
                playerLevel++;
                Debug.Log("Reached next level: " + playerLevel);
                currIndex++;

            }
        }
        //if reached a new high
        if (GameManager.instance.highestLevel < playerLevel)
        {
            GameManager.instance.levelUp(playerLevel);
            GameManager.instance.highestLevel = playerLevel;
        }

        GameManager.instance.exp = currExp;
        GameManager.instance.updateExp(xpLevels[currIndex]);
        playerText.text = "Level: " + playerLevel.ToString();

    }
}
