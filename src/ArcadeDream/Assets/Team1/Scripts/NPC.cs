//
//Last edited: 9/6/2019
//Author(s): Noah Lin
//Version 1.0
//Last changes: 
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public Transform ChatBackground;
    public Transform NPCCharacter;

    //Name of NPC
    public string Name;

    [TextArea(5, 8)]
    public string[] Dialog;

    private DialogSystem dialogSystem;
    private int heightAboveCharacter = 175; //adjust to fit the text box nicely above the character 

    // Start is called before the first frame update
    void Start()
    {
        dialogSystem = FindObjectOfType<DialogSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ChatBackground.position = Camera.main.WorldToScreenPoint(NPCCharacter.position + Vector3.up * 7f);
        Vector3 pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        pos.y += heightAboveCharacter;
        ChatBackground.position = pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogSystem>().EnterRangeOfNPC();

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "NPC")
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogSystem.Names = Name;
            dialogSystem.dialogLines = Dialog;
            FindObjectOfType<DialogSystem>().NPCName();
        }
    }
    //When leaving the collider, the dialog box should disappear
    public void OnTriggerExit()
    {
        FindObjectOfType<DialogSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
