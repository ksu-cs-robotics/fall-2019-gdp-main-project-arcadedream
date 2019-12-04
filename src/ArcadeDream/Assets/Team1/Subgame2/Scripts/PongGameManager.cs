using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PongGameManager : MonoBehaviourPunCallbacks
{

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

    public GameObject singlePlayerAI;

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

    public Player pScript;

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





    }

    void Update()
    {
        if (pScript.start && !spawned)
        {
            spawned = true;
            photonView.RPC("spawnBall", RpcTarget.MasterClient);
        }

        /*
        //for singleplayer AI
        if (numberOfPlayers == 1)
        {
            singlePlayerAI.SetActive(true); //Sets the singplayer ai object active which activates the script
        }
        */

    }

    [PunRPC]
    void spawnBall()
    {
        if(GameObject.FindWithTag("ExtremePongBall") == null)
        {
            PhotonNetwork.Instantiate("Subgame2/Ball", Vector2.zero, Quaternion.identity, 0);
        }
        spawned = true;
        InvokeRepeating("SpawnPowerUp", spawnPowerups, spawnPowerups);
    }



        void SpawnPowerUp()
        {
            powerupChosen = UnityEngine.Random.Range(0, 6); // random number 0-6
            spawnPointChosen = UnityEngine.Random.Range(0, 8); // random number 0-7
            Debug.Log(powerupChosen);
            //spawn random powerup at random spawn point
            PhotonNetwork.Instantiate("Subgame2/"+powerup[powerupChosen].name, spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
            Debug.Log("Power up " + powerupChosen + " spawned at spawn point " + spawnPointChosen);
        }
    }
