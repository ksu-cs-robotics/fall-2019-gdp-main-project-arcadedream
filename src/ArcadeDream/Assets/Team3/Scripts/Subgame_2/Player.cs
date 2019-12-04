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
        }
        else if (GameObject.FindWithTag("paddle2") == null)
        {
            playerNumber = 2;
            PhotonNetwork.Instantiate("Subgame2/" + player2Prefab.name, BluePaddleSpawn.position, BluePaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie2", BlueGoalieSpawn.position, BlueGoalieSpawn.rotation);
        }
        else if (GameObject.FindWithTag("paddle2") == null)
        {
            playerNumber = 3;
            PhotonNetwork.Instantiate("Subgame2/" + player3Prefab.name, GreenPaddleSpawn.position, GreenPaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie3", GreenGoalieSpawn.position, GreenGoalieSpawn.rotation);
        }
        else if (GameObject.FindWithTag("paddle2") == null)
        {
            playerNumber = 4;
            PhotonNetwork.Instantiate("Subgame2/" + player4Prefab.name, YellowPaddleSpawn.position, YellowPaddleSpawn.rotation, 0);
            PhotonNetwork.Instantiate("Subgame2/Goalie4", YellowGoalieSpawn.position, YellowGoalieSpawn.rotation);
       }
    }


    private void FixedUpdate()
    {

      
        if (!start && !move_on && Input.GetKeyDown(KeyCode.F)) //Force start for testing purposes
        {

            //PhotonNetwork.Instantiate("Subgame2/Ball", Vector2.zero, Quaternion.identity, 0);
            //move_on = true;


            start = true;
            photonView.RPC("activateMove", RpcTarget.All);
        } 
    }


    [PunRPC]
    void activateMove()
    {
        start = true;
        move_on = true;
        Debug.Log("Game Start");
    }


}
