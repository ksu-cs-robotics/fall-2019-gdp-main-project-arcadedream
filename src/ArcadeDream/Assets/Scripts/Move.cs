using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Joseph Trussell
//8/31/13
//low effort test for movment

public class Move : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
        // Update is called once per frame
        void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Movement(h, v);
    }

    void Movement(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }
}
