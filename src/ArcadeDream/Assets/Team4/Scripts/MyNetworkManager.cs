using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MyNetworkManager : NetworkManager
{

    public bool serverOn = false;
    public bool clientOn = false;
    public bool connected = false;

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("A client has connected to the server: " + conn);
        base.OnServerConnect(conn);
    }


    public new void StartServer()
    {
        base.StartServer();
        serverOn = true;
    }

    public new void StartClient()
    {
        base.StartClient();
        clientOn = true;
    }

    public void ConnectToInternalServer()
    {
        if (base.client != null && !base.client.isConnected)
        {
            base.client.Connect("localhost", 7777);
        }
        connected = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
