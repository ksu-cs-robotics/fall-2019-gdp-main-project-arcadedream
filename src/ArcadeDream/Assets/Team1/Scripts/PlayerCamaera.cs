using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCamaera : NetworkBehaviour { 

    public GameObject cameraPrefab;



    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = Instantiate(cameraPrefab, this.transform);

        CameraFollow cf = cam.GetComponent<CameraFollow>();

        cf.SetTarget(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
