using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
// This is placed on each Goal Zone object in the game and when the ball enters it a point is given to the last player who touched the ball

public class GoalLivesManager : NetworkBehaviour //Not lives points now
{
    public GameObject[] goals;
    Text pointsText;
    [SyncVar] public int points;
    string input;
    // Start is called before the first frame update
    void Start()
    {
        pointsText = GetComponentInChildren<Text>();
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
    }

    // Last player that touched ball gets the point
    // When a player touches the ball the color of the player is left on the ball
    // So we give the point to the player of the color of the ball
    public void GainPoint(Color goalColor) 
    {
        foreach (GameObject goal in goals)
        {
            if(goal.GetComponent<SpriteRenderer>().color == goalColor)
            {
                goal.GetComponent<GoalLivesManager>().points++;
                
            }
        }
    }
}
