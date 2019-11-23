using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


namespace PHOTONSHIT {
    public class GameManager : MonoBehaviourPunCallbacks
    {

        [Tooltip("Player Prefab to load in")]
        [SerializeField]
        GameObject playerPrefab;
        private string currentGameCreation;



        // Start is called before the first frame update
        void Start()
        {
            if(playerPrefab == null)
            {
                Debug.Log("You suck my guy. We need a PlayerPrefab");
            }
            else
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void StartGame(string name)
        {
            Debug.Log("StartGame Called");
            if(name == "XeonicFleet")
            {
                Debug.Log("Xeonic Fleet To Begin");
                PhotonNetwork.LeaveRoom(false);
                Debug.Log("Left Room");
                currentGameCreation = "XeonicFleet";
            }

            if (name == "BloodRun")
            {
                Debug.Log("BloodRun To Begin");
                PhotonNetwork.LeaveRoom(false);
                Debug.Log("Left Room");
                currentGameCreation = "BloodRun";
            }

            if (name == "ExtremePong")
            {
                Debug.Log("ExtremePong To Begin");
                PhotonNetwork.LeaveRoom(false);
                Debug.Log("Left Room");
                currentGameCreation = "ExtremePong";
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster Called in GameManager");
            PhotonNetwork.JoinRoom(currentGameCreation);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("No room avaliable for " + currentGameCreation + "\nCreating one now");
            PhotonNetwork.CreateRoom(currentGameCreation, new RoomOptions { MaxPlayers = 2});

        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Sucssefully Joined Room " + currentGameCreation);
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Trying to Load a level but we are not the Master Client");
            }
            PhotonNetwork.LoadLevel(currentGameCreation);
        }

    }
}
