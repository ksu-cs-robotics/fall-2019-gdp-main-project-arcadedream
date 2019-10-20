using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    public GameObject Shooter { get; set; }
    private bool isOnScreen_m = false;

    private void Start()
    {
        Shooter = gameObject.transform.parent.gameObject;
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
                    Destroy(gameObject);
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
                    Destroy(gameObject);
                    break;
                }
            case "1upPowerup":
                {
                    Destroy(gameObject);
                    break;
                }
            case "LaserPowerup":
                {
                    Destroy(gameObject);
                    break;
                }
            case "RapidFirePowerup":
                {
                    Destroy(gameObject);
                    break;
                }
            case "TopGunPowerup":
                {
                    Destroy(gameObject);
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
            Destroy(gameObject);
            isOnScreen_m = false;
        }
    }
}
