using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    [Tooltip("Game Version")]
    [SerializeField]
    string gameVersion = "1";

    /// <summary>
    /// Used to connect to the Photon Cloud Server
    /// </summary>
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinRoom("MainLobby");
        }
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to Load a level but we are not the Master Client");
        }
        Debug.LogFormat("Loading Main Lobby");
        PhotonNetwork.LoadLevel("Main");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No Room Avaliable Creating MainLobby");

        PhotonNetwork.CreateRoom("MainLobby", new RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);

        //LoadArena();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Sucssefully Joined Room");
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to Load a level but we are not the Master Client");
        }
        PhotonNetwork.LoadLevel("Main");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");

        PhotonNetwork.JoinRoom("MainLobby");
    }

    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }

}
