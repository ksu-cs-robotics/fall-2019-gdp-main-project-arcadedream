using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the gameover screen, will display how many points you earned and what place you were in
/// Author: Lew Fortwangler
/// </summary>
public class GameoverScreen : MonoBehaviour
{
    public GameObject gameoverUI;
    public Text scoreText;
    public Text placementText;

    private void Start()
    {
        gameoverUI.SetActive(false);
    }

    private void Update()
    {
        if (gameoverUI.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            //Change scene back to main arade
        }

        if(gameoverUI.activeInHierarchy == true)
        {
            setValues();
        }
    }

    private void setValues()
    {
        //for integration:
        //set scoreText to player score
        //set placementText to player placement
    }
}
