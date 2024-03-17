using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceReactor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        


    }

    public Sprite[] sprites;

    private void OnEnable()
    {
        GameManager.instance.gameEvents.onSliderStepChange += changeItem;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onSliderStepChange -= changeItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeItem(int val)
    {
       
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[val];
    }
}
