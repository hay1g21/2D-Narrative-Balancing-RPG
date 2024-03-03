using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    public Text goldText;
    public Text expText;

    public Text itemText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRewards()
    {
        goldText.text = "Gold: " + GameManager.instance.enemyData.GetComponent<OverworldEnemy>().getGold();
        expText.text = "Exp: " + GameManager.instance.enemyData.GetComponent<OverworldEnemy>().getExp();

        //do items later
    }

    //activates when object is set active, so when the winscreen appears

    private void OnEnable()
    {
        //Debug.Log("PrintOnEnable: script was enabled");
        
        if (GameManager.instance.gameState.Equals("VICTORY"))
        {
            setRewards();
        }
        
    }
    private void OnDisable()
    {
        //Debug.Log("PrintOnEnable: script was disbaled");
    }



}
