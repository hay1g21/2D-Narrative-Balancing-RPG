using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public AudioSource sound;

    public GameObject sliderMenu;
    public Slider slider;

    public GameObject startButt;
    public GameObject settingsButt;
    public GameObject quitButt;
    public GameObject title;

    public int startLevel; //which scene the first level is on

    public void startGame()
    {
        //so sound plays
        sound.Play();
        //StartCoroutine(waitForSound("Start"));
        sliderMenu.SetActive(true);
        startButt.SetActive(false);
        settingsButt.SetActive(false);
        title.SetActive(false);
        quitButt.SetActive(false);
        //load after finish
        slider.Select();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            sliderMenu.SetActive(false);
            startButt.SetActive(true);
            settingsButt.SetActive(true);
            title.SetActive(true);
            quitButt.SetActive(true);
            startButt.GetComponent<Button>().Select();
            sound.Play();
        }
    }
    public void actuallyStartGame()
    {
        sound.Play();
        StartCoroutine(waitForSound("Start"));
    }

    public void settings()
    {
        //nothing atm
        sound.Play();
    }
    public void exitGame()
    {
        //so sound plays
        sound.Play();
        StartCoroutine(waitForSound("Quit"));
    }

    IEnumerator waitForSound(string action)
    {
        //Wait Until Sound has finished playing
        while (sound.isPlaying)
        {
            yield return null;
        }

        //Auidio has finished playing, disable GameObject
        if (action.Equals("Start"))
        {
            GameObject.Find("SliderCanvas").GetComponent<SliderScript>().toggleMenu();
            GameManager.instance.toOverworld(startLevel);
        }else if (action.Equals("Quit"))
        {
            //quits
            Application.Quit();
        }
        
    }
}


