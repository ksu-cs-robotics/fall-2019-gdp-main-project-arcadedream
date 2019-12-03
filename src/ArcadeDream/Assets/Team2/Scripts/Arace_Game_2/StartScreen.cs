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
    public GameObject pTracker;

    private bool displayed_m = false;

    private void Start()
    {
        startUI.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0;
        //pTracker = GameObject.Find("PlayerTrack");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startUI.SetActive(false);
            gameplayUI.SetActive(true);
            pTracker.SetActive(true);
            Time.timeScale = 1;

            //
            // code to take away required amount of coins from player
            //
        }

        if(Input.GetKeyDown("c") && displayed_m == false && gameplayUI.activeInHierarchy == true)
        {
            displayed_m = true;
            controlsUI.SetActive(true);
        }
        else if(Input.GetKeyDown("c") && displayed_m == true && gameplayUI.activeInHierarchy == true)
        {
            displayed_m = false;
            controlsUI.SetActive(false);
        }
    }
}
