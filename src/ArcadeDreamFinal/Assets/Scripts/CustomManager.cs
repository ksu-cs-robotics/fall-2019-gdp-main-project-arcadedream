using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class CustomManager : NetworkManager

{ 

    public Player p;
    public GameObject playerPaddle;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        p.players++;

        /*
          if (p.players == 1) playerPrefab.GetComponent<GoalieMovement>().Init(1);
          else if (p.players == 2) playerPrefab.GetComponent<GoalieMovement>().Init(2);
          else if (p.players == 3) playerPrefab.GetComponent<GoalieMovement>().Init(3);
          else if (p.players == 4) playerPrefab.GetComponent<GoalieMovement>().Init(4);
          */

        if (p.totalPlayers >= p.players)
        {

            base.OnServerAddPlayer(conn, playerControllerId);
            GameObject paddle = playerPaddle;
            if (p.players == 1)
            {
                paddle = (GameObject)Instantiate(playerPaddle, PongGameManager.RedPaddleSpawn, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(paddle, conn);
            }
            else if (p.players == 2)
            {
                paddle = (GameObject)Instantiate(playerPaddle, PongGameManager.BluePaddleSpawn, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(paddle, conn);

            }
            else if (p.players == 3)
            {
                paddle = (GameObject)Instantiate(playerPaddle, PongGameManager.GreenPaddleSpawn, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(paddle, conn);
            }
            else if (p.players == 4)
            {
                paddle = (GameObject)Instantiate(playerPaddle, PongGameManager.YellowPaddleSpawn, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(paddle, conn);
            }
 
        }
        else
        {
            p.players--;
            Debug.Log("To many players, unable to join");
        }
        
        

    }


    public override void OnClientConnect(NetworkConnection conn)
    {

        base.OnClientConnect(conn);
        StartCoroutine(assign());

    }
   

    IEnumerator assign()
    {
        yield return new WaitForSeconds(.5f);
        p.open();
    }
    
}

