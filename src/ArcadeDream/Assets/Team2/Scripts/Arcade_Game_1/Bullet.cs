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
        if (Shooter.gameObject.tag == other.gameObject.tag)
            return;

        switch (other.gameObject.tag)
        {
            case "Player" :
                {
                    // other.gameObject.GetComponent<PlayerShip>().HEALTH 
                    break;
                }
            case "Enemy":
                {
                    // other.gameObject.GetComponent<Enemy>().HEALTH 
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
