using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements basic generic functionality of a bullet. Can be used by any gameobject
/// Author(s): Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // Stores a reference to the gameobject that shot the bullet
    GameObject Shooter { get; set; }

    void OnTriggerEnter(Collider other)
    {
        // This will ensure enemies cannot damager other enemies, and players cannot damage each other
        if (Shooter.gameObject.tag == other.gameObject.tag)
            return;

        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    break;
                }
            case "Enemy":
                {
                    other.gameObject.GetComponent<EnemyHealth>().TakeDamage(); 
                    break;
                }
            case "Obstacle":
                {
                    // other.gameObject.GetComponent<Obstacle>().HEALTH 
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
