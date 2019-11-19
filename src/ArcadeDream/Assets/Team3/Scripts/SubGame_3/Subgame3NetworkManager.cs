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
    public GameObject Player { get; set; }
    public short PlayerControllerId { get; set; }
    public bool isActive { get; set; }

    private PlayerInfo() { }
    public PlayerInfo(NetworkConnection conn, short playerControllerId, bool isactive) : this()
    {
        Conn = conn;
        PlayerControllerId = playerControllerId;
        isActive = isactive;
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
        Initialize();
    }

    private void Initialize()
    {
        isReady = false;
        isRunning = false;
        numPlayers = 0;
        _clientConnections = new List<PlayerInfo>();
    }

    // the override keywords are commented out now until this is fully functioning
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var active = true;
        if (numPlayers >= LobbyInfo.MAXPLAYERS || isRunning) { active = false; }

        // Call base function, and add this new connection to the list of connections inside the lobby
        ++numPlayers;
        _clientConnections.Add(new PlayerInfo(conn, playerControllerId, active));

        // If there is the requisite number of players in the lobby...
        isReady = (numPlayers >= LobbyInfo.MINPLAYERS) && !isRunning;       
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        --numPlayers;
        _clientConnections.RemoveAll((p) => p.Conn == conn);

        // Remove actual gameobject of the player

        // If there is the requisite number of players in the lobby..
        isReady = (numPlayers >= LobbyInfo.MINPLAYERS) && !isRunning;
    }
    public override void OnStopHost()
    {
        base.OnStopHost();
        Initialize();
    }

    public void StartGame()
    {
        foreach (var player in _clientConnections)
        {           
            // base.OnServerAddPlayer(player.Conn, player.PlayerControllerId);

            if (player.isActive)
            {
                // Get where to actually spawn this player from base.GetStartPosition()
                var playerSpawn = GetStartPosition();
                var playerObject = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);

                // Get the reference to its physical gameObject in PlayerInfo so it'll be easy to map objects to connections
                player.Player = playerObject;

                NetworkServer.AddPlayerForConnection(player.Conn, playerObject, player.PlayerControllerId);
            }
        }

        isReady = false;
        isRunning = true;
    }
    public void EndGame()
    {
        var activePlayers = _clientConnections.FindAll((p) => p.isActive);
        foreach (var player in activePlayers)
        {
            // base.OnServerRemovePlayer
            NetworkServer.Destroy(player.Player);
        }

        isReady = true;
        isRunning = false;
    }
}
