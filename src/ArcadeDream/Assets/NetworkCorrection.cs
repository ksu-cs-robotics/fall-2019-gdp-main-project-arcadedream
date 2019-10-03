using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCorrection : NetworkBehaviour
{

    [SyncVar] Vector3 rotation;

    public override void OnStartServer()
    {
        rotation = new Vector3(0, 90, 0);
    }

    public override void OnStartClient()
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
}
