using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Photon.Pun;
using Photon.Realtime;

namespace PHOTONSHIT
{

    public class ExitManager : MonoBehaviourPunCallbacks
    {
        public void LeaveRoom()
        {
            if(NetworkManager.singleton != null)
            {
                NetworkManager.singleton.StopServer();
                NetworkManager.singleton.StopHost();
                NetworkManager.singleton.StopClient();
            }
            PhotonNetwork.LeaveRoom();
            Debug.Log("Left room");
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
    }
}
