using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject enemy;
    private GameObject player;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject magicPrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;


    public void SelectAttack(string btn)
    {
        GameObject victim = player;
        if (tag == "Player")
        {
            victim = enemy;
        }
        if (btn.Equals("melee"))
        {
            //call an attack
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("Melee");
        }
        else if (btn.Equals("magic"))
        {
            Debug.Log("Magic");
            magicPrefab.GetComponent<AttackScript>().Attack(victim);
  
        }
        else
        {
            //meleeAttack.GetComponent<AttackScript>().Attack(victim)
            Debug.Log("Res");
            GameObject.Find("GameControllerObject").GetComponent<GameController>().switchAttack();
        }
    }
    void Awake()
    {
        //get player and enemt
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
