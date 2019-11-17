using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniGameManager : NetworkBehaviour
{
    public Stack<string> xeonicGames;
    public Stack<string> pongGames;
    public Stack<string> bloodRunGames;

    [SerializeField] private bool runningMiniGame_m;


    // Start is called before the first frame update
    void Start()
    {
        runningMiniGame_m = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMiniGame(string gameName)
    {
        if (isClient && !isServer)
        {
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StartHost();
            NetworkManager.singleton.ServerChangeScene(gameName);
        }
    }

    public string GetIPAddressForGame(string gameName)
    {
        string IPString = "";

        if(gameName == "XeonicFleet")
        {
            if(xeonicGames.Count != 0)
            {
                IPString = xeonicGames.Pop();
            }
            else
            {
                StartMiniGame(gameName);
            }
        }
        else if(gameName == "ExtremePong")
        {
            if (xeonicGames.Count != 0)
            {
                IPString = pongGames.Pop();
            }
            else
            {
                StartMiniGame(gameName);
            }
        }
        else if(gameName == "BloodRun")
        {
            if (xeonicGames.Count != 0)
            {
                IPString = bloodRunGames.Pop();
            }
            else
            {
                StartMiniGame(gameName);
            }
        }

        return IPString;
    }
}
