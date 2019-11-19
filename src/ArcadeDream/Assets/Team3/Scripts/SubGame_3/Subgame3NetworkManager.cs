using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

/// <summary>
/// Holds static information regarding a subgame 3 lobby.
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public static class LobbyInfo
{
    public const int MAXPLAYERS = 4;
    public const int MINPLAYERS = 1;
}

public class PlayerInfo
{
    public NetworkConnection Conn { get; set; }
    public short PlayerControllerId { get; set; }

    private PlayerInfo() { }
    public PlayerInfo(NetworkConnection conn, short playerControllerId) : this()
    {
        Conn = conn;
        PlayerControllerId = playerControllerId;
    }
}

/// <summary>
/// Custom networking manager to create a spectate/lobby feature for subgame3
/// Author(s): Josh Dotson
/// Version: 1
/// Note: Insipred by Team 3's networking manager for subgame 2
/// </summary>
public class Subgame3NetworkManager : NetworkManager
{
    public bool isReady { get; set; }
    public bool isRunning { get; set; }
    public new uint numPlayers { get; set; }

    private List<PlayerInfo> _clientConnections;

    public Subgame3NetworkManager()
    {
        // there can be a max of 4 players within a game
        isReady = false;
        isRunning = false;
        numPlayers = 0;
        _clientConnections = new List<PlayerInfo>();
    }

    // the override keywords are commented out now until this is fully functioning
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        try
        {
            if (numPlayers >= LobbyInfo.MAXPLAYERS) { throw new Exception("Server Is Full!"); }
            if (isRunning) { throw new Exception("The Match Has Already Begun!"); }

            // Call base function, and add this new connection to the list of connections inside the lobby
            ++numPlayers;
            _clientConnections.Add(new PlayerInfo(conn, playerControllerId));

            // If there is the requisite number of players in the lobby...
            isReady = (numPlayers >= LobbyInfo.MINPLAYERS) && !isRunning;
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: {ex.Message}");
            return;
        }
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);

        --numPlayers;
        _clientConnections.RemoveAll((p) => p.PlayerControllerId == player.playerControllerId );

        // Remove actual gameobject of the player
        
        // If there is the requisite number of players in the lobby..
        isReady = (numPlayers >= LobbyInfo.MINPLAYERS) && !isRunning;
    }

    public /*override*/ void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public void StartGame()
    {
        foreach (var player in _clientConnections)
        {
            base.OnServerAddPlayer(player.Conn, player.PlayerControllerId);
            
            // Get where to actually spawn this player from base.GetStartPosition()
            // var playerSpawn = GetStartPosition();

            // Spawn in the actual player prefab, and assign it client authority to this particular connection
            // var playerObject = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
            // NetworkServer.SpawnWithClientAuthority(playerObject, player.Conn);
        }

        isReady = false;
        isRunning = true;
    }
    public void EndGame()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            NetworkServer.Destroy(player);
        }

        isReady = true;
        isRunning = false;
    }
}
