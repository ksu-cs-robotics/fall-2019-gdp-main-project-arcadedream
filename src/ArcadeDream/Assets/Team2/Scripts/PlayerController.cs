using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Implements basics controls for WASD, and jumping movements
/// Author: Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour //NetworkBehaviour
{
    [SerializeField] public float WALKSPEED = 3.0f;
    [SerializeField] public float RUNSPEED = 5.0f;
    [SerializeField] public float JUMPSPEED = 5.0f;

    protected Rigidbody rigidbody_m;
    protected List<Collider> colliders_m;

    public PlayerController()
    {
        colliders_m = new List<Collider>();
    }

    #region Start, and Update Functions
    // Start is called before the first frame update
    void Start()
    {
        rigidbody_m = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movement = new Vector3();
        var jump = new Vector3();

        // Get raw input values
        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized * Time.deltaTime;

        // Choose movement multiplier based on whether or not the sprint input is used
        if (!Input.GetAxis("Sprint").Equals(0)) { movement *= RUNSPEED; }
        else { movement *= WALKSPEED; }
        
        jump.Set(0f, Input.GetAxisRaw("Jump"), 0f);
        
        // Process user movement input...
        if (!movement.Equals(Vector3.zero))
        {
            Vector3 newPosition = transform.position + movement;
            Vector3 newPositionVector = newPosition - transform.position;
            newPositionVector.y = 0f;

            var newRotation = Quaternion.LookRotation(newPositionVector, Vector3.up);
            var interpolatedRotation = Quaternion.Slerp(newRotation, transform.rotation, Time.deltaTime * 30);

            rigidbody_m.MovePosition(transform.position + movement);
            rigidbody_m.MoveRotation(interpolatedRotation);
        }
        
        // Process user jumping input...
        if (!jump.Equals(Vector3.zero))
        {
            if (rigidbody_m.velocity.y == 0)
            {
                rigidbody_m.AddRelativeForce(Vector3.up * JUMPSPEED, ForceMode.Impulse);
            }          
        }

        // Basic environmental interact function
        if (!Input.GetAxis("Submit").Equals(0))
        {
            var game = colliders_m.Find((c) => c.gameObject.tag == "Game");

            if (game.Equals(default(Collider)))
            {
                //colliders_m[0].gameObject.GetComponent<>
            }
            else
            {
                //game.gameObject.GetComponent<>
            }
        }
    }
    #endregion

    // When object comes in contact with the player, add it to the list of nearby objects
    private void OnTriggerEnter(Collider other)
    {
        if (!colliders_m.Contains(other))
        {
            colliders_m.Add(other);
        }
    }

    // When object leaves the proximity of the player, remove it from the list of nearby objects
    private void OnTriggerExit(Collider other)
    {
        colliders_m.Remove(other);
    }
}

