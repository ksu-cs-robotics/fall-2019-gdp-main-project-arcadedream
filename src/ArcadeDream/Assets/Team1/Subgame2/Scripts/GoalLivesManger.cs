using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is placed on each Goal Zone object in the game and when the ball enters it a life should be lost
// This is getting the lives from the GameManager script

public class GoalLivesManger : MonoBehaviour
{
    Text livesText;
    int lives;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        PongGameManager pongGameManager = gameManager.GetComponent<PongGameManager>();
        livesText = GetComponentInChildren<Text>();
        lives = pongGameManager.lives;
        Debug.Log(pongGameManager.lives);
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = lives.ToString();
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives = lives - 1;
        }
    }
}
