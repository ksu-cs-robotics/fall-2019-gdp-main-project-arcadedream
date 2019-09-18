using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeGame : MonoBehaviour
{
    public Transform ArcadeGameUI;
    public Transform ArcadeObject;
    public int ArcadeNumber = 0; //= 0 if NPC



    private PlayArcadeGame playArcadeGame;
    private int heightAboveCharacter = 175; //adjust to fit the text box nicely above the character 

    public GameObject thePlayer;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        playArcadeGame = FindObjectOfType<PlayArcadeGame>();

    }

    // Update is called once per frame
    void Update()
    {
        ArcadeGameUI.position = Camera.main.WorldToScreenPoint(ArcadeObject.position + Vector3.up * 7f);
        Vector3 pos = Camera.main.WorldToScreenPoint(ArcadeObject.position);
        pos.y += heightAboveCharacter;
        ArcadeGameUI.position = pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<ArcadeGame>().enabled = true;
        FindObjectOfType<PlayArcadeGame>().EnterRangeOfArcade();

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "ArcadeGame1")
        {
            this.gameObject.GetComponent<ArcadeGame>().enabled = true;
            playArcadeGame.arcadeNumber = ArcadeNumber;
            FindObjectOfType<PlayArcadeGame>().ArcadeName();
            //Disabling player movement
            PlayerMovement playerMovement = thePlayer.GetComponent<PlayerMovement>();
            speed = playerMovement.speed;
            playerMovement.speed = 0.0f;
        }
    }
    //When leaving the collider, the dialog box should disappear
    public void OnTriggerExit()
    {
        FindObjectOfType<PlayArcadeGame>().OutOfRange();
        this.gameObject.GetComponent<ArcadeGame>().enabled = false;
    }



}
