using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector director; //current director
    //public GameObject controlPanel;

    public int cutId; //id of cutscene
    public bool canPlay;

    public GameObject[] cutscenes;

    public string[] speeches;
    public string[] speakers;

    public GameObject hud;

    public MultiDimensionalString[] speechSeq;
    public MultiDimensionalString[] speakerSeq;



    private void Awake()
    {
        /*
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
        */


    }

    public void OnEnable()
    {
        //subscribe to events
        GameManager.instance.gameEvents.onCutSceneTrigger += startCutscene;
        
    }

    private void OnDisable()
    {
        //unsub
        GameManager.instance.gameEvents.onCutSceneTrigger -= startCutscene;
      
    }

    //hides whatever starts panel i guess
    private void Director_Played(PlayableDirector obj)
    {
        //controlPanel.SetActive(false);
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        //controlPanel.SetActive(true);
        Debug.Log("End of cutscene" + cutId);
        GameManager.instance.cutscenesPlayed.Add(cutId.ToString());
        director.stopped -= Director_Stopped;
        GameManager.instance.player.GetComponent<PlayerMovement>().allowMove();
        //hud.SetActive(true);
        hud.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, -300);
        GameManager.instance.cutScenePlaying = false;
    }

    //starts the timeline


    public void startCutscene(int id)
    {
        GameObject cutsceneToPlay = null;
        //get id of cutscene (timeline)
        foreach(GameObject gameobj in cutscenes)
        {
            if(gameobj.GetComponent<Cutscene>().cutId == id)
            {
                cutsceneToPlay = gameobj;
                cutId = id;
            }
        }
        //get director
        if(cutsceneToPlay != null)
        {
            Debug.Log("MOVE");
            GameManager.instance.player.GetComponent<PlayerMovement>().switchMovement();
            speechSeq = cutsceneToPlay.GetComponent<Cutscene>().speeches;
            speakerSeq = cutsceneToPlay.GetComponent<Cutscene>().speakers;
            
            director = cutsceneToPlay.GetComponent<PlayableDirector>();
            director.stopped += Director_Stopped;
        }
        StartTimeLine();
        //then play
       
    }
    public void StartTimeLine()
    {
        GameManager.instance.cutScenePlaying = true;
        //hud.SetActive(false);
        hud.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -300);
        director.Play();
    }

    void Update()
    {


    }
    public void SignalCall()
    {

        //Debug.Log("Signal receievd");
        //start dialgoue
        //Debug.Log(speeches[0]);
        //DialogueManager.instance.setText(speeches, speakers, 0.05f);
        Debug.Log("SIGNAL SIGNAL SIGNAL PLEASE");
        DialogueManager.instance.setCutDiag(speechSeq,speakerSeq);
        DialogueManager.instance.cutSceneDiag();




    }
}
