using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{

    public int id; //id of cutscene to play

    public bool touched = false;

    void Start()
    {
        //make it invis
        SpriteRenderer ren = gameObject.GetComponent<SpriteRenderer>();
        ren.color = new Vector4(ren.color.r, ren.color.g, ren.color.b, 0);


    }
    //used to store any colliders of an object touching the level exit's trigger region
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        //checks if the object has the player tag
        if (collision.gameObject.CompareTag("Player") && !touched)
        {
            touched = true;
            //if it does, the player has touched the exit and the next level is loaded
            GameManager.instance.gameEvents.startCutscene(id);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
