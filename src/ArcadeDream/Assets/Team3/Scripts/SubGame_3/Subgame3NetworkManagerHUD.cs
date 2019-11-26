using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

/// <summary>
/// Defines a custom network manager Id component just for use with subgame 3
/// Author: Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Subgame3NetworkManager))]
public class Subgame3NetworkManagerHUD : MonoBehaviour
{
    private Subgame3NetworkManager manager;
    private MainCamera camera;
    private Subgame3GameTimer timer;

    [SerializeField] public bool showGUI = true;
    [SerializeField] public int offsetX;
    [SerializeField] public int offsetY;

    // Runtime variable
    bool showServer = false;
    bool isHosting = false;
    // bool isStarted = false;

    private string _errorMessage;

    void Awake()
    {
        // Get the network manager components of this components parent gameObject
        manager = GetComponent<Subgame3NetworkManager>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        timer = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Subgame3GameTimer>();

        _errorMessage = string.Empty;
    }

    void Update()
    {
        // To allow any of the hotkey shortcuts, we must first check to see if the GUI is active...
        if (!showGUI)
            return;  

        // If the server, nor the client are active...
        if (!NetworkClient.active && !NetworkServer.active)
        {
            timer.runTimer = false;

            // Hot key shortbut for the host button
            if (Input.GetKeyDown(KeyCode.H)) { manager.StartHost(); isHosting = true; }
            // Hotkey shortcut for the client button
            if (Input.GetKeyDown(KeyCode.J)) { manager.StartClient(); isHosting = false; }
        }

        // If the server (host) is active and this client has connected to it...
        if (NetworkServer.active && NetworkClient.active)
        {
            // Hotkey shortcut for the stop button
            if (Input.GetKeyDown(KeyCode.X)) { manager.StopHost(); }
        }
    }

    void OnGUI()
    {
        if (!showGUI)
            return;

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        int spacing = 24;

        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Host Game (H)"))
            {
                manager.StartHost();
                isHosting = true;
            }
            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Game (J)"))
            {
                manager.StartClient();
                isHosting = false;
            }
            ypos += spacing;

            manager.networkAddress = GUI.TextField(new Rect(xpos, ypos - 4, 200, 20), manager.networkAddress);
            ypos += spacing;

            GUI.Label(new Rect(xpos, ypos, 200, 20), _errorMessage);
            ypos += spacing;
        }
        else
        {
            if (NetworkServer.active)
            {
                GUI.Label(new Rect(xpos, ypos, 300, 20), $"Server Port: {manager.networkPort}");
                ypos += spacing;
            }
            if (NetworkClient.active)
            {
                GUI.Label(new Rect(xpos, ypos, 300, 20), $"Client IP: {manager.networkAddress} Client Port: {manager.networkPort}");
                ypos += spacing;
            }

            // if (!camera.spawned)
            {
                string statusLabel = string.Empty;
                statusLabel = (isHosting) ? $"#Players: {manager.numPlayers} / {LobbyInfo.MAXPLAYERS}" : "Waiting For Host...";

                // Shows how many players are currently in the lobby
                GUI.Label(new Rect(xpos, ypos, 200, 25), statusLabel);
                ypos += spacing;
            }

            if (NetworkServer.active || NetworkClient.active)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Exit Game"))
                {
                    if (isHosting) { manager.StopHost(); manager.EndGame(); }
                    else { manager.StopClient(); }

                    // manager.StopHost();
                    isHosting = false;
                    // isStarted = false;
                }
            }
            ypos += spacing;
        }

        // If this player is hosting, and the server is ready, you may start the game!
        if (isHosting && manager.isReady && GUI.Button(new Rect(xpos, ypos, 200, 20), "Start Game!"))
        {
            manager.StartGame();
            // isStarted = true;
        }
    }
}