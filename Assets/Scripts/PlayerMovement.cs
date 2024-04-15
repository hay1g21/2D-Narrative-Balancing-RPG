using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 originalSize;
    public bool moving = true;

    private Vector2 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moving = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
        //moving = true;
    }

    // Update is called once per frame - called based on framerate good for inputs
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate() //consistent, good for physics
    {
       
      Move();
       
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        
        moveDir = new Vector2(moveX, moveY).normalized; //capped to 1
    }

    void Move()
    {
        if (moving)
        {
            if(moveDir.x > 0)
            {
                transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
            }
            else if(moveDir.x < 0)
            {
                
                transform.localScale = new Vector3(originalSize.x * 1, originalSize.y, originalSize.z);
            }
            rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        

    }
    
    public void switchMovement()
    {
        //Debug.Log("How many times are you being called");
        moving = !moving;
    }
    

    public void allowMove()
    {
        //Debug.Log("How many times are you being called to allow?");
        moving = true;
    }

    public void abortMove()
    {
        //Debug.Log("How many times are you being called to abort?");
        moving = false;
    }
}
