using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    GameObject Shooter { get; set; }

    private Bullet()
    {

    }
    public Bullet(GameObject playerObject) : this()
    {
        Shooter = playerObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If other is not an Enemy, it will throw an exception
        try
        {
            // other.gameObject.GetComponent<Enemy>().
        }
        finally
        {
            // Deal the damage to the other Collider and then destory this gameobject
        }
    }
}
