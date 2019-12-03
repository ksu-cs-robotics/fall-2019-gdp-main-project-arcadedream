using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManagerXeonic : MonoBehaviourPunCallbacks
{
    [Tooltip("Player Prefab to load in")]
    [SerializeField]
    GameObject playerPrefab;
    private string currentGameCreation;



    // Start is called before the first frame update
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.Log("You suck my guy. We need a PlayerPrefab");
        }
        else if(PlayerShip.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-10f, 0f, 3f), Quaternion.identity, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master Again");
        PhotonNetwork.JoinRoom("MainLobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Well Fuck. I guess this is an error");
        PhotonNetwork.CreateRoom("MainLobby", new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Sucssefully Joined Room MainLobby");
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to Load a level but we are not the Master Client");
        }
        PhotonNetwork.LoadLevel("Main");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left room");
    }
}
