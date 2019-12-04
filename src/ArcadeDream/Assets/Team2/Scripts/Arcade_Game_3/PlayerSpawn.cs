using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Bloodrun
{
    public class PlayerSpawn : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        public GameObject player;

        public GameObject spawnPoint;
        public GameObject spawnPoint2;
        public GameObject spawnPoint3;
        public GameObject spawnPoint4;
        public GameObject countdownObj;

        public Text countdown;

        private int minPlayers = 1;
        private int maxPlayers = 4;
        [SerializeField]
        private int currentPlayers;
        private string game;

        void Start()
        {
            currentPlayers = PhotonNetwork.PlayerList.Length;
            if (currentPlayers == 0)
            {
                PhotonNetwork.Instantiate(player.name, spawnPoint.transform.position, Quaternion.identity, 0);
            }
            if (currentPlayers == 1)
            {
                PhotonNetwork.Instantiate(player.name, spawnPoint2.transform.position, Quaternion.identity, 0);
            }
            if (currentPlayers == 2)
            {
                PhotonNetwork.Instantiate(player.name, spawnPoint3.transform.position, Quaternion.identity, 0);
            }
            if (currentPlayers == 3)
            {
                PhotonNetwork.Instantiate(player.name, spawnPoint4.transform.position, Quaternion.identity, 0);
            }
        }

        void Update()
        {
            if (currentPlayers == maxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }

        public void StartGame(string name)
        {
            if (name == "BloodRun")
            {
                PhotonNetwork.LeaveRoom(false);
                game = "BloodRun";
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster Called in GameManager");
            PhotonNetwork.JoinRoom(game);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("No room avaliable for " + game + "\nCreating one now");
            PhotonNetwork.CreateRoom(game, new RoomOptions { MaxPlayers = 4 });

        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Sucssefully Joined Room " + game);
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Trying to Load a level but we are not the Master Client");
            }
            PhotonNetwork.LoadLevel(game);
        }
    }
}
