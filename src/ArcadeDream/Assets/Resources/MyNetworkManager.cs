using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace PHOTONSHIT
{
    public class MyNetworkManager : NetworkManager
    {
        void OnDestroy()
        {
            Debug.Log("YEET");
            StopServer();
        }
    }
}
