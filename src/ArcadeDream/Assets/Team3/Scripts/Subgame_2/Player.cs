using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks
{

    public int playerNumber;
    public  int players = 0;
    public int totalPlayers;
    public bool start = false;
    public bool move_on = false;

    public Text startButtonText;
    public Button startButton;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;
    public GameObject player4Prefab;

    public static Transform RedPaddleSpawn; //Screen points for players
    public static Transform BluePaddleSpawn;
    public static Transform GreenPaddleSpawn;
    public static Transform YellowPaddleSpawn;


    public static Transform RedGoalieSpawn; //Screen points for players
    public static Transform BlueGoalieSpawn;
    public static Transform GreenGoalieSpawn;
    public static Transform YellowGoalieSpawn;

    private PhotonView pv;

    private bool x = false;

    public void Start()
    {
        RedPaddleSpawn = GameObject.Find("RedPaddlePoint").GetComponent<Transform>();
        BluePaddleSpawn = GameObject.Find("BluePaddlePoint").GetComponent<Transform>();
        GreenPaddleSpawn = GameObject.Find("GreenPaddlePoint").GetComponent<Transform>();
        YellowPaddleSpawn = GameObject.Find("YellowPaddlePoint").GetComponent<Transform>();

        RedGoalieSpawn = GameObject.Find("1").GetComponent<Transform>();
        BlueGoalieSpawn = GameObject.Find("4").GetComponent<Transform>();
        GreenGoalieSpawn = GameObject.Find("3").GetComponent<Transform>();
        YellowGoalieSpawn = GameObject.Find("2").GetComponent<Transform>();

        pv = GetComponent<PhotonView>();

        StartCoroutine(assignPlayer());

    }


    IEnumerator assignPlayer()
    {
        yield return new WaitForSeconds(.5f);

        if (GameObject.FindWithTag("paddle1") == null)
        {
            
            playerNumber = 1;
            PhotonNetwork.Instantiate("Subgame2/"+player1Prefab.name, RedPaddleSpawn.position, RedPaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie1", RedGoalieSpawn.position, RedGoalieSpawn.rotation);
            GameObject g = PhotonNetwork.Instantiate("Subgame2/RedGoal", new Vector2(5, -5), Quaternion.Euler(0,0,135));

            
        }
        else if (GameObject.FindWithTag("paddle2") == null)
        {
            playerNumber = 2;
            PhotonNetwork.Instantiate("Subgame2/" + player2Prefab.name, BluePaddleSpawn.position, BluePaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie2", BlueGoalieSpawn.position, BlueGoalieSpawn.rotation);
            GameObject g = PhotonNetwork.Instantiate("Subgame2/BlueGoal", new Vector2(-5, 5), Quaternion.Euler(0,0,315));

        }
        else if (GameObject.FindWithTag("paddle3") == null)
        {
            playerNumber = 3;
            PhotonNetwork.Instantiate("Subgame2/" + player3Prefab.name, GreenPaddleSpawn.position, GreenPaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie3", GreenGoalieSpawn.position, GreenGoalieSpawn.rotation);
            GameObject g = PhotonNetwork.Instantiate("Subgame2/GreenGoal", new Vector2(-5, -5), Quaternion.Euler(0,0,45));

        }
        else if (GameObject.FindWithTag("paddle4") == null)
        {
            playerNumber = 4;
            PhotonNetwork.Instantiate("Subgame2/" + player4Prefab.name, YellowPaddleSpawn.position, YellowPaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie4", YellowGoalieSpawn.position, YellowGoalieSpawn.rotation);
            PhotonNetwork.Instantiate("Subgame2/YellowGoal", new Vector2(5, 5), Quaternion.Euler(0,0,225));
        }
    }



    public void startGame()
    {
           photonView.RPC("activateMove", RpcTarget.All);
        
       // else StartCoroutine(buttonText());

    }

    IEnumerator buttonText()
    {
        startButtonText.text = "Please wait for another player!";
        yield return new WaitForSeconds(3f);
        startButtonText.text = "Start Game";
    }

    IEnumerator countdown()
    {
        move_on = true;
        startButtonText.text = "3";
        yield return new WaitForSeconds(1f);
        startButtonText.text = "2";
        yield return new WaitForSeconds(1f);
        startButtonText.text = "1";
        yield return new WaitForSeconds(1f);
        startButtonText.text = "Go!";
        start = true;
        yield return new WaitForSeconds(1f);
        Destroy(startButton);

            
    }

    [PunRPC]
    void activateMove()
    {

        StartCoroutine(countdown());
        Debug.Log("Game Start");
    }


}
