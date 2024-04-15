using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGate : MonoBehaviour
{

    public int levelTo;
    // Start is called before the first frame update


    void Start()
    {
        SpriteRenderer ren = gameObject.GetComponent<SpriteRenderer>();
        //ren.color = new Vector4(ren.color.r, ren.color.g, ren.color.b, ren.color.a);
        ren.color = new Vector4(ren.color.r, ren.color.g, ren.color.b, 0);


    }
    //used to store any colliders of an object touching the level exit's trigger region
    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the object has the player tag
        if (collision.gameObject.CompareTag("Player"))
        {
            //if it does, the player has touched the exit and the next level is loaded
            GameManager.instance.toOverworld(levelTo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
