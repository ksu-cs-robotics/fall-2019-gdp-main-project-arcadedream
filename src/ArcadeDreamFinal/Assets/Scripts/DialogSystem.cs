using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public GameObject dialogGUI;
    public Transform dialogBoxUI;

    public float letterDelay = 0.1f;
    public float letterMultipler = 0.5f;
    public KeyCode DialogInput = KeyCode.E;
    public string Names;
    public string[] dialogLines;

    public bool letterIsMultiplied = false;
    public bool dialogActive = false;
    public bool dialogEnded = false;
    public bool outOfRange = true;

    //Can add an audio effect of text popping up
    //public AudioClip audioClip;
    //public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When player enters the collider this is called.
    //If dialog was previously being played it will be reset.
    //Player has only just entered the collider, they have not pressed the interaction key.
    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        dialogGUI.SetActive(true); 
        if (dialogActive == true)
        {
            dialogGUI.SetActive(false);
        }
    }
    //Called when player both in collider range and interaction key is pressed while in range.
    public void NPCName()
    {
        outOfRange = false;
        dialogBoxUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogActive)
            {
                dialogActive = true;
                //Starts a coroutine which is a repeating function that continues to execute across multiple frames until completion. 
                StartCoroutine(StartDialog());
            }
        }
        StartDialog();
    }
    //Once called will continues until completion.
    private IEnumerator StartDialog()
    {
        if (outOfRange == false)
        {
            int dialogLength = dialogLines.Length;
            int currentDialogIndex = 0;

            while(currentDialogIndex < dialogLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    //Entering new coroutine
                    StartCoroutine(DisplayString(dialogLines[currentDialogIndex++]));
                    if (currentDialogIndex >= dialogLength)
                    {
                        dialogEnded = true;
                    }
                }
                yield return 0;
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogInput) && dialogEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogEnded = false;
            dialogActive = false;
            DropDialog();
        }
    }
    //creates the the effect of typing out the string.
    private IEnumerator DisplayString(string stringToDisplay)
    {
        if(outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogText.text = "";
            while (currentCharacterIndex < stringLength)
            {
                dialogText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;
                if(currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultipler); //if player presses the input the string appears faster.
                        
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay); //waits a certain time before the next letter appears 
                    }
                }
                else
                {
                    dialogEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogEnded = false;
            letterIsMultiplied = false;
            dialogText.text = "";
        }
    }

    public void DropDialog()
    {
        dialogGUI.SetActive(false);
        dialogBoxUI.gameObject.SetActive(false);
    }

    //When the player is out of range all the dialog UI will not be seen.
    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogActive = false;
            StopCoroutine(StartDialog());
            dialogGUI.SetActive(false);
            dialogBoxUI.gameObject.SetActive(false);
        }
    }
}
