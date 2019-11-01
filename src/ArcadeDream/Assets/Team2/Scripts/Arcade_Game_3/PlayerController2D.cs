using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Created to test movement of players in a 2D networked enviorment
/// Author(s): Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : NetworkBehaviour // NetworkBehaviour
{
    // This will likely be set from a local config file
    public string PlayerUsername;

    [SerializeField] public float WALKSPEED = 3.0f;
    [SerializeField] public float RUNSPEED = 5.0f;
    [SerializeField] public float JUMPSPEED = 5.0f;

    protected Rigidbody2D rigidbody_m;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody_m = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var movement = new Vector2();
        var jump = new Vector2();

        if (isLocalPlayer)
        {
            // Get 2D movement...
            movement.Set(Input.GetAxisRaw("Horizontal"), 0f);
            movement = movement.normalized * Time.deltaTime;

            // If sprinting, increase the movement speed...
            if (!Input.GetAxisRaw("Sprint").Equals(0)) { movement *= RUNSPEED; }
            else { movement *= WALKSPEED; }

            // Get any jumping movement...
            jump.Set(0f, Input.GetAxisRaw("Jump"));

            // Move
            transform.Translate(movement);

            // Process jumping input
            if (!jump.Equals(Vector3.zero))
            {
                if (rigidbody_m.velocity.y == 0)
                {
                    rigidbody_m.AddRelativeForce(Vector3.up * JUMPSPEED, ForceMode2D.Impulse);
                }
            }
        }
    }
}