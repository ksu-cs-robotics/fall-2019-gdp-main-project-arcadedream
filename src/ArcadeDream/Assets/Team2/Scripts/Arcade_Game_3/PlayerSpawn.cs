using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    public Text countdown;

    private int minPlayers = 2;
    private int maxPlayers = 4;
    private int currentPlayers = 0;
    private int seconds = 5;

    private bool gameStarted = false;

    void Start()
    {
        countdown.text = "5";
        if(currentPlayers == 0)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint.transform.position, Quaternion.identity, 0);
            ++currentPlayers;
        }
    }

    void Update()
    {
        if(gameStarted == false)
        {
            SpawnPlayers();
        }

        if(currentPlayers >= minPlayers)
        {
            //activate start button, button starts countDown
            gameStarted = true;
        }
        if(currentPlayers == maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }
    
    private void SpawnPlayers()
    {
        if (currentPlayers == 1)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint2.transform.position, Quaternion.identity, 0);
            ++currentPlayers;
        }
        if (currentPlayers == 2)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint3.transform.position, Quaternion.identity, 0);
            ++currentPlayers;
        }
        if (currentPlayers == 3)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint4.transform.position, Quaternion.identity, 0);
            ++currentPlayers;
        }
    }

    private IEnumerable CountDown()
    {
        yield return new WaitForSeconds(1);
        countdown.text = $"{seconds}";
        --seconds;
    }
}
