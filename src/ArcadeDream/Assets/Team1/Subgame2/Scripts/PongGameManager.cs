
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PongGameManager : MonoBehaviour
{
    public BallMovement ball; //effectively ball prefab
    public PlayerPaddleMovement playerPaddle; //effectively playerpaddle prefab

    public Transform[] spawnPoint;
    public int lives = 10;
    public float spawnPowerups = 10f;
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    public GameObject powerup4;
    public GameObject powerup5;
    public GameObject powerup6;
    public GameObject powerup7;
    int powerupChosen;
    //public GameObject gameBackground;

    public static Vector2 bottomLeft; //Screen points that are the edges of the game play area
    public static Vector2 topRight;
    public static Vector2 bottomRight; 
    public static Vector2 topLeft;

    // Start is called before the first frame update
    void Start()
    {
        //Convert screen's pixel coordinate into game coordinate 
        //Will later change to be only on black background
        //bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomLeft = GameObject.Find("BottomLeft").GetComponent<Transform>().position;
        bottomRight = GameObject.Find("BottomRight").GetComponent<Transform>().position;
        topRight = GameObject.Find("TopRight").GetComponent<Transform>().position;
        topLeft = GameObject.Find("TopLeft").GetComponent<Transform>().position;


        //Create ball
        Instantiate(ball);

        //Create 4 paddles
        PlayerPaddleMovement player1paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
        PlayerPaddleMovement player2paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
        PlayerPaddleMovement player3paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
        PlayerPaddleMovement player4paddle = Instantiate(playerPaddle) as PlayerPaddleMovement;
        player1paddle.Init(1); 
        player2paddle.Init(2); 
        player3paddle.Init(3); 
        player4paddle.Init(4);

        InvokeRepeating("Spawn", spawnPowerups, spawnPowerups);
    }

    void Spawn()
    {
        powerupChosen = Random.Range(1, 7);

        if (powerupChosen == 1)
        {
             Instantiate(powerup1, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 2)
        {
            Instantiate(powerup2, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 3)
        {
            Instantiate(powerup3, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 4)
        {
            Instantiate(powerup4, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 5)
        {
            Instantiate(powerup5, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 6)
        {
            Instantiate(powerup6, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        if (powerupChosen == 7)
        {
            Instantiate(powerup7, spawnPoint[0].position, spawnPoint[0].rotation);
        }
       


    }
}
