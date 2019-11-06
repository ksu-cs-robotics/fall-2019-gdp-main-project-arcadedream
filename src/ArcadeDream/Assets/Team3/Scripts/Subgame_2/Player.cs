using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{

    public int playerNumber;
    [SyncVar] public int players = 0;
    [SyncVar] public int totalPlayers;
    public static bool active = false;
    [SyncVar] public bool move_on = false;

    public Button startButton;
    



    public void Choose1()
    { 
        playerNumber = 1;
    }

    public void Choose2()
    {
        playerNumber = 2;
    }

    public void Choose3()
    {
        playerNumber = 3;
    }
    

    public void Choose4()
    {
        playerNumber = 4;
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Choose1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Choose2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Choose3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Choose4();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerNumber = 0;
        }

        if(!active && players == totalPlayers && Input.GetKeyDown(KeyCode.Alpha0) && isServer) //For Starting full game
        {
            active = true;
            move_on = true;
            Debug.Log("Game Start");
        }

        if (!active && Input.GetKeyDown(KeyCode.F) && isServer) //Force start for testing purposes
        {
            active = true;
            move_on = true;
            Debug.Log("Game Start");
        } 

    }




    public void open()
    {
        if(playerNumber == 0)
             playerNumber = players;
        Debug.Log("Server Start/Join. Player " + players + " joined");
    }


}
