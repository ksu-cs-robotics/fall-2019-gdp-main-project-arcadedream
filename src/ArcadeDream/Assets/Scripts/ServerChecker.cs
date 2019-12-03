using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerChecker : MonoBehaviour
{
    public GameObject NetworkManger_m;
    private MyNetworkManager nm;

    // Start is called before the first frame update
    void Start()
    {
        nm = NetworkManger_m.GetComponent<MyNetworkManager>();
    }



    // Update is called once per frame
    void Update()
    {
        Image image = gameObject.GetComponent<Image>();
        if (gameObject.name == "ServerOn")
        {
            if (nm.serverOn)
            {
                image.color = Color.green;
            }
            else
            {
                image.color = Color.red;
            }
        }
        else if(gameObject.name == "ClientOn")
        {
            if (nm.clientOn)
            {
                image.color = Color.green;
            }
            else
            {
                image.color = Color.red;
            }
        }
        else if (gameObject.name == "ConnectInternalOn")
        {
            if (nm.connected)
            {
                image.color = Color.green;
            }
            else
            {
                image.color = Color.red;
            }
        }
    }
}
