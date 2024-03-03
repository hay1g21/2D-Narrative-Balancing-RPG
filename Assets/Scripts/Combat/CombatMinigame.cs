using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMinigame : MonoBehaviour
{

    public float barChangeSpeed = 1f;

    
    public GameObject powerFill;

    
    public GameObject powerBar;

    private Transform powerTransform;

    public float lowlim; //bet 1-100
    public int highlim; //between 1-100

    public bool finished;
    public bool getBonus;
    private Vector2 powerScale;

    public bool powerBarOn;
    public float maxPower;
    public float power;
    
    private float xNewPowerScale;

    // Start is called before the first frame update
    private void Awake()
    {
        powerTransform = powerFill.GetComponent<RectTransform>();
        powerScale = powerFill.transform.localScale;
        powerBarOn = false;
        power = 0;
        maxPower = 100;
        finished = false;
        
    }
    void Start()
    {

        powerTransform = powerFill.GetComponent<RectTransform>();
        powerScale = powerFill.transform.localScale;
        powerBar.SetActive(false);
    }

    public void startMinigame()
    {
        Debug.Log("Starting");
        powerBar.SetActive(true);
        powerBarOn = true;
        getBonus = false;
        power = 0;
        xNewPowerScale = 0; //updates ratio of health bar                 //Debug.Log("Health scale now " + xNewHealthScale);
        powerFill.transform.localScale = new Vector2(xNewPowerScale, powerScale.y);
        StartCoroutine(updatePowerBar());
    }
    IEnumerator updatePowerBar()
    {
        while (powerBarOn)
        {
            power += barChangeSpeed;
            if (power > maxPower)
            {
                power = 0;
            }
            xNewPowerScale = (power / maxPower);
            powerFill.transform.localScale = new Vector2(xNewPowerScale, powerScale.y);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("Hello");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Advance")) && powerBarOn)
        {
            //finish minigame
            powerBarOn = false;
            Debug.Log("Attack at power: " + power);
            powerBar.SetActive(false);
            if(power >= lowlim && power <= highlim)
            {
                Debug.Log("Perfect attack");
                getBonus = true;
            }
            finished = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse");
           
            startMinigame();
        }
    }
}
