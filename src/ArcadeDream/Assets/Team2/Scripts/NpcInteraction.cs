using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements interaction with NPCs. When the player is close, display text on what button to press to initiate dialogue
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>
public class NpcInteraction : MonoBehaviour
{
    public GameObject INTERACTTEXT;  //text element to be displayed when near a NPC. Ex/ "[X] To Talk"

    void Start()
    {
        INTERACTTEXT.SetActive(false);  //text element is not displayed initially
    }

    private void OnTriggerEnter(Collider Player)    //when player is nearby NPC, text instructing how to interact is displayed
    {
        if(Player.gameObject.tag == "Player")   //Player object must have the tag "Player"
        {
            INTERACTTEXT.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if(Player.gameObject.tag == "Player")
        {
            INTERACTTEXT.SetActive(false);
        }
    }
}
