using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PongGameManager : NetworkBehaviour
{
    //public BallMovement ball; // effectively ball prefab
    //public PlayerPaddleMovement playerPaddle; // playerpaddle prefab
    public GoalieMovement goalie; // goalie prefab

    public GameObject[] goals;
    public Transform[] spawnPoint;
    public float spawnPowerups = 10f;
    public GameObject[] powerup; //all powerups
    //public GameObject gameBackground;

    [HideInInspector] public int powerupChosen;
    [HideInInspector] public int spawnPointChosen;

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

    //Team3 addition
    public GameObject ball;
    public GameObject playerPaddle;
    private GameObject serverBall = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //Corners of the play space
        bottomLeft = GameObject.Find("BottomLeft").GetComponent<Transform>().position;
        bottomRight = GameObject.Find("BottomRight").GetComponent<Transform>().position;
        topRight = GameObject.Find("TopRight").GetComponent<Transform>().position;
        topLeft = GameObject.Find("TopLeft").GetComponent<Transform>().position;

        //Get spawn points for players
        RedPaddleSpawn = GameObject.Find("RedPaddlePoint").GetComponent<Transform>().position;
        BluePaddleSpawn = GameObject.Find("BluePaddlePoint").GetComponent<Transform>().position;
        GreenPaddleSpawn = GameObject.Find("GreenPaddlePoint").GetComponent<Transform>().position;
        YellowPaddleSpawn = GameObject.Find("YellowPaddlePoint").GetComponent<Transform>().position;

        //Create ball
        //Instantiate(ball);

       
    }

    void Update()
    {
        if (Player.active && !spawned)
        {
            if (isServer)
            {
                serverBall = (GameObject)Instantiate(ball);
                NetworkServer.Spawn(serverBall);
                InvokeRepeating("SpawnPowerUp", spawnPowerups, spawnPowerups); // Starting in (spawnPowerups) seconds.
            }

            spawned = true;
        }
     }



        void SpawnPowerUp()
        {
            powerupChosen = UnityEngine.Random.Range(0, 7); // random number 0-6
            spawnPointChosen = UnityEngine.Random.Range(0, 8); // random number 0-7
            Debug.Log(powerupChosen);
            //spawn random powerup at random spawn point
            GameObject PU = (GameObject)Instantiate(powerup[powerupChosen], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
            NetworkServer.Spawn(PU);
            Debug.Log("Power up " + powerupChosen + " spawned at spawn point " + spawnPointChosen);
        }
    }



