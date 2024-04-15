using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tutScreen;

    void Start()
    {
        if (!GameManager.instance.firstBattle)
        {
            tutScreen.SetActive(true);
        }
        else
        {
            tutScreen.SetActive(false);
        }

    }



    // Update is called once per frame
    void Update()
    {
        // Check for mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            //get rid of this!
            GameManager.instance.firstBattle = true;
            Destroy(gameObject);
        }
    }
}
