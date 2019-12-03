using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShot : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                CmdSpawnCube();
            }
        }
    }

    [Command]
    void CmdSpawnCube()
    {
        GameObject cube = Instantiate(projectile);

        NetworkServer.Spawn(cube);

        cube.transform.position = gameObject.transform.position;
    }
}
