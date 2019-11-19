using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

/// <summary>
/// Custom networking manager to create a spectate/lobby feature for subgame3
/// Author(s): Josh Dotson
/// Version: 1
/// Note: Insipred by Team 3's networking manager for subgame 2
/// </summary>
public class Subgame3NetworkManager : NetworkManager
{
    public Player player;
    public GameObject playerPrefab;

    public void Awake()
    {
        // there can be a max of 4 players within a game
        // player.totalPlayers = 4;
    }

    // the override keywords are commented out now until this is fully functioning
    public /*override*/ void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (player.players == player.totalPlayers)
        {
            Debug.Log("The Server is Full");
            return;
        }
        
        // One new player joined this lobby
        ++player.players;     
    }

    /*public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
    }*/

    public /*override*/ void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }
}
