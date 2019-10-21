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
    }
}
