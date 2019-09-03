using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements basics controls for WASD, and jumping movements
/// Author: Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float WALKSPEED = 3.0f;
    [SerializeField] public float JUMPSPEED = 5.0f;

    protected Rigidbody rigidbody_m;
    protected List<Collider> colliders_m;

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

        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized * WALKSPEED * Time.deltaTime;
        jump.Set(0f, Input.GetAxisRaw("Jump"), 0f);
        
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

        if (!jump.Equals(Vector3.zero))
        {
            if (rigidbody_m.velocity.y == 0)
            {
                rigidbody_m.AddRelativeForce(Vector3.up * JUMPSPEED, ForceMode.Impulse);
            }          
        }

        if (Input.GetAxis("Submit").Equals(0))
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

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders_m.Contains(other))
        {
            colliders_m.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders_m.Remove(other);
    }
}

