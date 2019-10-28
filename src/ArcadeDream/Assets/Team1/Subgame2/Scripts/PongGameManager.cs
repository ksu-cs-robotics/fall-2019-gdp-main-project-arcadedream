﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PongGameManager : MonoBehaviour
{
    public BallMovement ball; // effectively ball prefab
    public PlayerPaddleMovement playerPaddle; // playerpaddle prefab
    public GoalieMovement goalie; // goalie prefab

    public GameObject[] goals;
    public Transform[] spawnPoint;
    public float spawnPowerups = 10f;
    public GameObject[] powerup; //all powerups
    //public GameObject gameBackground;

    [HideInInspector]public int powerupChosen;
    [HideInInspector]public int spawnPointChosen;

    public int numberOfPlayers = 0; // 2 or 4
    bool spawned = false;

    public static Vector2 bottomLeft; //Screen points that are the edges of the game play area
    public static Vector2 topRight;
    public static Vector2 bottomRight; 
    public static Vector2 topLeft;

    public static Vector2 RedSpawn; //Screen points for goalies
    public static Vector2 BlueSpawn;
    public static Vector2 GreenSpawn;
    public static Vector2 YellowSpawn;

    public static Vector2 RedPaddleSpawn; //Screen points for players
    public static Vector2 BluePaddleSpawn;
    public static Vector2 GreenPaddleSpawn;
    public static Vector2 YellowPaddleSpawn;

    // Start is called before the first frame update
    void Start()
    {

        //Corners of the play space
        bottomLeft = GameObject.Find("BottomLeft").GetComponent<Transform>().position;
        bottomRight = GameObject.Find("BottomRight").GetComponent<Transform>().position;
        topRight = GameObject.Find("TopRight").GetComponent<Transform>().position;
        topLeft = GameObject.Find("TopLeft").GetComponent<Transform>().position;

        //Get spawn points for goalies
        RedSpawn = GameObject.Find("RedGoalie").GetComponent<Transform>().position;
        BlueSpawn = GameObject.Find("BlueGoalie").GetComponent<Transform>().position;
        GreenSpawn = GameObject.Find("GreenGoalie").GetComponent<Transform>().position;
        YellowSpawn = GameObject.Find("YellowGoalie").GetComponent<Transform>().position;

        //Get spawn points for players
        RedPaddleSpawn = GameObject.Find("RedPaddlePoint").GetComponent<Transform>().position;
        BluePaddleSpawn = GameObject.Find("BluePaddlePoint").GetComponent<Transform>().position;
        GreenPaddleSpawn = GameObject.Find("GreenPaddlePoint").GetComponent<Transform>().position;
        YellowPaddleSpawn = GameObject.Find("YellowPaddlePoint").GetComponent<Transform>().position;

        //Create ball
        Instantiate(ball);

        InvokeRepeating("SpawnPowerUp", spawnPowerups, spawnPowerups); // Starting in (spawnPowerups) seconds.
                                                                // a powerup will be spawned every (spawnPowerup) seconds

    }

    void Update()
    {
        
        if (!spawned)
        {
        

            //Create 2 Paddle / Goal / Goalie
            PlayerPaddleMovement player1paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
            PlayerPaddleMovement player2paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
            player1paddle.Init(1);
            player2paddle.Init(2);
            GoalieMovement player1goalie = Instantiate(goalie) as GoalieMovement;
            GoalieMovement player2goalie = Instantiate(goalie) as GoalieMovement;
            player1goalie.Init(1);
            player2goalie.Init(2);
            goals[0].SetActive(true);
            goals[1].SetActive(true);
            if (numberOfPlayers == 4)
            {
                //+2 Paddle / Goal / Goalie
                PlayerPaddleMovement player3paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
                PlayerPaddleMovement player4paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
                player3paddle.Init(3);
                player4paddle.Init(4);
                GoalieMovement player3goalie = Instantiate(goalie) as GoalieMovement;
                GoalieMovement player4goalie = Instantiate(goalie) as GoalieMovement;
                player3goalie.Init(3);
                player4goalie.Init(4);
                goals[2].SetActive(true);
                goals[3].SetActive(true);
            }
            spawned = false;
        }
        
    }

    void SpawnPowerUp()
    {
        powerupChosen = Random.Range(0, 7); // random number 0-6
        spawnPointChosen = Random.Range(0, 8); // random number 0-7
        Debug.Log(powerupChosen);
        //spawn random powerup at random spawn point
        Instantiate(powerup[powerupChosen], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);

        Debug.Log("Power up " + powerupChosen + " spawned at spawn point " + spawnPointChosen);
    }
}
