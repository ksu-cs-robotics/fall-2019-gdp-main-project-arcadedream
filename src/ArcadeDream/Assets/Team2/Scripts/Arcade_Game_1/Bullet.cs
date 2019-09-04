using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject firer;

    private Bullet()
    {

    }
    public Bullet(ref GameObject playerObject) : this()
    {
        firer = playerObject;
    }

    private void OnTriggerEnter(ref Collider other)
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
