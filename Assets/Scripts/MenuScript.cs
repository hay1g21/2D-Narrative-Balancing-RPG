using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource sound;

    public void startGame()
    {
        //so sound plays
        sound.Play();
        StartCoroutine(waitForSound("Start"));
        //load after finish


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
            GameManager.instance.toOverworld(1);
        }else if (action.Equals("Quit"))
        {
            //quits
            Application.Quit();
        }
        
    }
}


