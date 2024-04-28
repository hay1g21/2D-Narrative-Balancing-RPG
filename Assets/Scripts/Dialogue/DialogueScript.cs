using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/*
   
   Adapted from:
   "5 Minute DIALOGUE SYSTEM in UNITY Tutorial" by BMo (YouTube, 2021)
   Video URL: https://www.youtube.com/watch?v=8oTYabhj248
*/

public class DialogueScript : MonoBehaviour
{

    //vars

    public TextMeshProUGUI textComponent;

    public TextMeshProUGUI speakerComponent;

    [TextArea]
    public string[] lines;
    public string[] speakers; //who is talking each line

    public float txtSpeed;

    

    private int index;

    // Start is called before the first frame update
    void Start()
    {
       
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Advance"))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; //fills out lines like skipping dia
            }
        }
    }

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        index = 0;
        speakerComponent.text = speakers[index];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray()) //takes string and breaks into char array
        {
            //types a chara one at a time
            textComponent.text += c;
           
            yield return new WaitForSecondsRealtime(txtSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            speakerComponent.text = speakers[index];
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1; //resume time
            DialogueManager.instance.active = false; //inactive
        }
    }

    //sets up dialogue for usage
    public void setDialogue(string[] lines, string[] speakers, float speed)
    {
        
        this.lines = lines;
        this.speakers = speakers;
        this.txtSpeed = speed;

        //end
    }
}
