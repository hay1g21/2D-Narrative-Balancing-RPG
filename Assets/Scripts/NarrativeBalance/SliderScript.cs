using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderScript : MonoBehaviour
{


    [SerializeField] private Slider slider;

    [SerializeField]
    private int val;

    public GameObject contentParent;

    [SerializeField] private TextMeshProUGUI currentSliderText;
    [SerializeField] private TextMeshProUGUI currentSliderDescText;

    
    public string[] positions;

    [TextArea]
    public string[] descs;

    

    public bool changed; //when exiting the menu deciding what changed

    // Start is called before the first frame update
    void Start()
    {

        val = (int)GameManager.instance.balanceLevel;

        currentSliderText.text = positions[val];
        currentSliderDescText.text = descs[val];
        GameManager.instance.gameEvents.sliderStepChange(val);

        slider.onValueChanged.AddListener((v) =>
        {
            Debug.Log("CHanged");
            currentSliderText.text = positions[(int)v];
            currentSliderDescText.text = descs[(int)v];
            val = (int)v;
            GameManager.instance.balanceLevel = val;
            changed = true;
            //GameManager.instance.gameEvents.sliderStepChange((int)v);
           
        });

        if (contentParent.activeInHierarchy)
        {
            contentParent.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("BalanceMenu") && !DialogueManager.instance.active && GameManager.instance.sceneType.Equals("overworld"))
        {
            toggleMenu();
        }

    }

    //for gamemanagers use
    public void setSlider(int v)
    {
        currentSliderText.text = positions[(int)v];
        currentSliderDescText.text = descs[(int)v];
        val = (int)v;
        GameManager.instance.balanceLevel = val;
        GameManager.instance.gameEvents.sliderStepChange(val);
        slider.value = val;
        Debug.Log("Slider value = " + val);
        
    }

    private void toggleMenu()
    {
        if (contentParent.activeInHierarchy)
        {
            if (changed)
            {
                GameManager.instance.gameEvents.sliderStepChange(val);
                changed = false;
            }

            contentParent.SetActive(false);
            // Time.timeScale = 1; //breaks key inputs btdubs
            
        }
        else
        //show
        {
            contentParent.SetActive(true);
            currentSliderText.text = positions[val];
            currentSliderDescText.text = descs[val];
            slider.value = val;
            slider.Select();
            //Time.timeScale = 0;



        }
        GameManager.instance.player.GetComponent<PlayerMovement>().switchMovement();

    }
}
