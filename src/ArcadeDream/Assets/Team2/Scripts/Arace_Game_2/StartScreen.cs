using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes to the gameplay once the player clicks to start
/// Will take away amount of coints required to play from the player (once that system is entirely functional)
/// Author: Lew Fortwangler
/// </summary>
public class StartScreen : MonoBehaviour
{
    public GameObject startUI;
    public GameObject gameplayUI;
    public GameObject controlsUI;

    private bool displayed_m = false;

    private void Start()
    {
        startUI.SetActive(true);
        gameplayUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startUI.SetActive(false);
            gameplayUI.SetActive(true);

            //
            // code to take away required amount of coins from player
            //
        }

        if(Input.GetKeyDown("c") && displayed_m == false)
        {
            displayed_m = true;
            controlsUI.SetActive(true);
        }
        else if(Input.GetKeyDown("c") && displayed_m == true)
        {
            displayed_m = false;
            controlsUI.SetActive(false);
        }
    }
}
