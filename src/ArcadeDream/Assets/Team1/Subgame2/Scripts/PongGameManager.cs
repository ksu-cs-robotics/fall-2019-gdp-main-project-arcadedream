using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    public BallMovement ball; //effectively ball prefab
    public PlayerPaddleMovement playerPaddle; //effectively playerpaddle prefab

    public int lives = 10;

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
    }
}
