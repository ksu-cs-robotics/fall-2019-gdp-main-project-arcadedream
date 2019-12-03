using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class subgame1UI : MonoBehaviourPunCallbacks
{
    GameObject player;
    PlayerShip playerShip;

    public Text scoreText;
    public Text livesText;
    //public Text stageText;

    public GameObject gameOverUI;
    public GameObject startScreenUI;
    //public GameObject levelClearUI;
    public GameObject victoryUI;
    public GameObject highscoreUI;
    public GameObject environment;

    bool gameover;
    float time = 2.0f;

    void Start()
    {
        player = GameObject.Find("PlayerShip");
        if (player != null)
        {
            playerShip = player.GetComponent<PlayerShip>();
        }
        gameover = false;
        Time.timeScale = 0; //pausing game
        startScreenUI.SetActive(true);


    }

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIChange();// not networked only happens on server RPC command to tell clients to change scenes


            }
       
        if (player != null)
        {
            scoreText.text = "Score " + playerShip.Points.ToString();
            livesText.text = "X " + playerShip.LIVES.ToString();

            if (playerShip.LIVES <= 0 && !gameover)
            {
                gameOverUI.SetActive(true);
                Time.timeScale = 0;
                gameover = true;
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerShip = player.GetComponent<PlayerShip>();
            }
        }
    }
    
    void UIChange()
    {
        startScreenUI.SetActive(false);
        Time.timeScale = 1; //playing game
        environment.SetActive(true);
    }
    //moving to highscore screen
    public void GameOverUIButton()
    {
        gameOverUI.SetActive(false);
        highscoreUI.SetActive(true);

    }

    //moving to highscore screen
    public void VictoryUIButton()
    {
        victoryUI.SetActive(false);
        highscoreUI.SetActive(true);

    }

    //moving back to main game lobby
    public void highscoreUIButton()
    {
        //
        //SceneManager.GetSceneByName("NAME OF MAIN LOBBY");
    }
}