using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayArcadeGame : MonoBehaviour
{
    public GameObject dialogGUI;
    public Transform arcadeUI;
    public KeyCode DialogInput = KeyCode.E;

    public bool arcadeActive = false;
    public bool outOfRange = true;

    public int subgameNumber;

    [HideInInspector]
    public int arcadeNumber;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Called when player both in collider range and interaction key is pressed while in range.
    public void ArcadeName()
    {
        outOfRange = false;
        arcadeUI.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!arcadeActive)
            {
                arcadeActive = true;
            }
        }
    }

    public void EnterRangeOfArcade()
    {
        outOfRange = false;
        dialogGUI.SetActive(true);
        if (arcadeActive == true)
        {
            dialogGUI.SetActive(false);
        }
    }

    //When the player is out of range all the arcade UI will not be seen.
    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            arcadeActive = false;
            dialogGUI.SetActive(false);
            arcadeUI.gameObject.SetActive(false);
        }
    }
}

