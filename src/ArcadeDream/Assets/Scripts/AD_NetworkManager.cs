using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Supress Deprication warning for NetworkManager
public class AD_NetworkManager : NetworkLobbyManager
{
    public GameObject playerCharacter;

    private void Start()
    {
        NetworkServer.Listen(7777);

        // Register events for server to listen for
        NetworkServer.RegisterHandler(MsgType.Connect, OnPlayerConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnPlayerDisconnect);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
        NetworkServer.RegisterHandler(MsgType.Error, OnError);

        Debug.Log("Server started");
    }

    private void OnPlayerConnect(NetworkMessage msg)
    {
        Debug.Log("Player Connected");
    }

    private void OnPlayerDisconnect(NetworkMessage msg)
    {
        Debug.Log("Player disconnected");
    }

    private void OnAddPlayer(NetworkMessage msg)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(msg.conn, player, 0);
        Debug.Log("Player spawn");
    }

    private void OnError(NetworkMessage msg)
    {
        Debug.Log("Error: " + msg);
    }
}
#pragma warning restore CS0618
