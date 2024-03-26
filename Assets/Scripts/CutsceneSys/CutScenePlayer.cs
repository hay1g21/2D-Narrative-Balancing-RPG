using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{

    public int id; //id of cutscene to play

    public bool touched = false;

    public List<int> balanceLevels;

    public bool forcedCut;

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onSliderStepChange += changeBalance;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onSliderStepChange -= changeBalance;
    }

    void Start()
    {
        //make it invis
        SpriteRenderer ren = gameObject.GetComponent<SpriteRenderer>();
        ren.color = new Vector4(ren.color.r, ren.color.g, ren.color.b, 0);
        

    }

    public void changeBalance(int val)
    {
        if (forcedCut && balanceLevels.Contains(val))
        {
            GameManager.instance.gameEvents.startCutscene(id);
            Destroy(gameObject);
        }
    }
    //used to store any colliders of an object touching the level exit's trigger region
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        //checks if the object has the player tag
        if (collision.gameObject.CompareTag("Player") && !touched && balanceLevels.Contains(GameManager.instance.balanceLevel))
        {
            touched = true;
            //if it does, the player has touched the exit and the next level is loaded
            GameManager.instance.gameEvents.startCutscene(id);
            Destroy(gameObject);
        }
    }

    
}
