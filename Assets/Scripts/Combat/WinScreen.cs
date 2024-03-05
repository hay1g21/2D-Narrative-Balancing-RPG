using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    public Text goldText;
    public Text expText;
    public Text resultText;

    public Text itemText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItemReward(string items)
    {
        itemText.text = "Items Gained: " + items;
    }
    public void setRewards()
    {
        resultText.text = "Victory";
        goldText.text = "Gold: " + GameManager.instance.enemyData.GetComponent<OverworldEnemy>().getGold();
        expText.text = "Exp: " + GameManager.instance.enemyData.GetComponent<OverworldEnemy>().getExp();

        
    }
    public void setLoss()
    {
        resultText.text = "Defeat";
        goldText.text = "The monsters have beaten your party!";
        expText.text = "You lost!";

    }


    //activates when object is set active, so when the winscreen appears

    private void OnEnable()
    {
        //Debug.Log("PrintOnEnable: script was enabled");
        
        if (GameManager.instance.gameState.Equals("VICTORY"))
        {
            setRewards();
        }else if (GameManager.instance.gameState.Equals(GameManager.LOSE_STATE))
        {
            setLoss();
        }
        
    }
    private void OnDisable()
    {
        //Debug.Log("PrintOnEnable: script was disbaled");
    }



}
