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
    public GameObject highscoreUI;
    public Text scoreText;
    public Text placementText;

    private int yourPlacement_m = 0;

    private void Start()
    {
        gameoverUI.SetActive(false);
        highscoreUI.SetActive(false);
    }

    private void Update()
    {
        if(highscoreUI.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            //change scene back to main arcade
        }
        if (gameoverUI.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            //Change scene back to main arade
        }

        if(gameoverUI.activeInHierarchy == true && Input.GetKeyDown("h")) //view highscore leaderboard
        {
            gameoverUI.SetActive(false);
            highscoreUI.SetActive(true);
        }

        if(gameoverUI.activeInHierarchy == true)
        {
            setValues();
        }
    }

    private void setValues()  //displays player score and placement on gameover screen
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("GoalZone");
        GameObject redGoal_m = goals[0]; GameObject blueGoal_m = goals[1];
        GameObject greenGoal_m = goals[2]; GameObject yellowGoal_m = goals[3];

        int redPoints_m = goals[0].GetComponent<GoalLivesManager>().points;
        int bluePoints_m = goals[1].GetComponent<GoalLivesManager>().points;
        int greenPoints_m = goals[2].GetComponent<GoalLivesManager>().points;
        int yellowPoints_m = goals[3].GetComponent<GoalLivesManager>().points;

        //assuming red paddle is YOU for now, need to wait on a system from team1 that will assign you a paddle
        //then I will be able to use "yourPaddle" to compare against the other paddles.

        scoreText.text = "" + redPoints_m;

        int[] otherPlayersScore = { bluePoints_m, greenPoints_m, yellowPoints_m };

        for (int i = 0; i < otherPlayersScore.Length - 1; ++i)
        {
            if(redPoints_m > otherPlayersScore[i])
            {
                yourPlacement_m++;
            }
        }

        if (yourPlacement_m == 0)
            placementText.text = "4th";
        else if (yourPlacement_m == 1)
            placementText.text = "3rd";
        else if (yourPlacement_m == 2)
            placementText.text = "2nd";
        else
            placementText.text = "1st";
    }
}
