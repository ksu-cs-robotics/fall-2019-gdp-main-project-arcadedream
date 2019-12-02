﻿using System.Collections;
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
        public GameObject button;

        public Text countdown;

        private int minPlayers = 1;
        private int maxPlayers = 4;
        private int seconds = 5;
        [SerializeField]
        private int currentPlayers;

        private bool gameStarted = false;

        void Start()
        {
            currentPlayers = PhotonNetwork.PlayerList.Length;
            button.SetActive(false);
            countdown.text = "5";
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
            if (currentPlayers >= minPlayers)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    button.SetActive(true);
                }
            }
            if (currentPlayers == maxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }

        public void Wrapper()
        {
            gameStarted = true;
            StartCoroutine("Countdown");
        }

        private IEnumerator CountDown()
        {
            yield return new WaitForSeconds(1);
            countdown.text = $"{seconds}";
            --seconds;
        }
    }
}
