using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    public GameObject Shooter { get; set; }
    private bool isOnScreen_m = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer || other == null || Shooter.gameObject.tag == other.gameObject.tag)
        {
            if (Shooter == null)
            {
                Debug.Log("Null");
            }
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    // other.gameObject.GetComponent<PlayerShip>().HEALTH 

                    NetworkServer.Destroy(gameObject);

                    break;
                }
            case "Enemy":
                {
                    var healthComponent = other.gameObject.GetComponent<EnemyHealth>();

                    healthComponent.TakeDamage();
                    Shooter.GetComponent<PlayerShip>().Points += healthComponent.SCOREVALUE;
                    NetworkServer.Destroy(gameObject);

                    break;
                }
            case "PowerupObstacle":
                {
                    // other.gameObject.GetComponent<Obstacle>().HEALTH 
                    NetworkServer.Destroy(gameObject);
                    break;
                }
            case "1upPowerup":
                {
                    NetworkServer.Destroy(gameObject);
                    break;
                }
            case "LaserPowerup":
                {
                    NetworkServer.Destroy(gameObject);
                    break;
                }
            case "RapidFirePowerup":
                {
                    NetworkServer.Destroy(gameObject);
                    break;
                }
            case "TopGunPowerup":
                {
                    NetworkServer.Destroy(gameObject);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //If the obstacle is visible on screen, set variable to true
    private void OnBecameVisible()
    {
        isOnScreen_m = true;
    }

    //If the obstacle is not visible on screen (but it was previously)
    //Then destroy object and set on screen variable to false
    private void OnBecameInvisible()
    {
        if (isOnScreen_m == true)
        {
            NetworkServer.Destroy(gameObject);
            isOnScreen_m = false;
        }
    }
}