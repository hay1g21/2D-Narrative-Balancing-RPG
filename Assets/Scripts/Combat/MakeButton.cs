using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{

    [SerializeField]
    private bool physical;
    private GameObject player;
    private GameObject GameControllerObj;

    public bool active;
    public void Awake()
    {
       
        GameControllerObj = GameObject.Find("GameControllerObject");
    }
    // Start is called before the first frame update
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //finds which button is pressed
    private void AttachCallback(string btn)
    {

        if (btn.Equals("atcBtn"))
        {
            if (GameControllerObj.GetComponent<GameController>().getAttack())
            {
                GameControllerObj.GetComponent<GameController>().switchAttack();
                player.GetComponent<FighterAction>().SelectAttack("melee");
            }

        }
        else if (btn.Equals("mgcBtn"))
        {
            if (GameControllerObj.GetComponent<GameController>().getAttack())
            {
                GameControllerObj.GetComponent<GameController>().switchAttack();
                player.GetComponent<FighterAction>().SelectAttack("magic");
            }

        }
        else if (btn.Equals("invBtn"))
        {
            if (active)
            {
                InventoryManager.instance.inventoryMenu.SetActive(false);
                active = false;
            }
            else
            {
                InventoryManager.instance.inventoryMenu.SetActive(true);
                active = true;
            }

        }
        else if (btn.Equals("exitBtn"))
        {
            GameManager.instance.toOverworld(GameManager.instance.prevScene);
        }
        else if (btn.Equals("Escape"))
        {
            GameManager.instance.toOverworld(GameManager.instance.prevScene);
        }
        else
        {
            if (GameControllerObj.GetComponent<GameController>().getAttack())
            {
                GameControllerObj.GetComponent<GameController>().switchAttack();
                player.GetComponent<FighterAction>().SelectAttack("res");
            }
        }
    }
    

}
