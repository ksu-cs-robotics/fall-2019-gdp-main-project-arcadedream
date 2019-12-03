using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneChanger : NetworkBehaviour
{
    // Start is called before the first frame update

    public GameObject nm;

    void Start()
    {
        nm = GameObject.Find("NetworkManager");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        NetworkManager.singleton.StartServer();
        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }
}
