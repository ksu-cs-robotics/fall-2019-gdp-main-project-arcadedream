using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            transform.Translate(-Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0f, -Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        }
    }
}
