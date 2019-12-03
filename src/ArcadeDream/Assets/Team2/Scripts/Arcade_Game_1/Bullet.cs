using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviourPunCallbacks
{
    public GameObject Shooter { get; set; }
    private bool isOnScreen_m = false;

    private void OnTriggerEnter(Collider other)
    {
        if ( other == null || Shooter.gameObject.tag == other.gameObject.tag)
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

                    PhotonNetwork.Destroy(gameObject);

                    break;
                }
            case "Enemy":
                {
                    var healthComponent = other.gameObject.GetComponent<EnemyHealth>();

                    healthComponent.TakeDamage();
                    Shooter.GetComponent<PlayerShip>().Points += healthComponent.SCOREVALUE;
                    PhotonNetwork.Destroy(gameObject);

                    break;
                }
            case "PowerupObstacle":
                {
                    // other.gameObject.GetComponent<Obstacle>().HEALTH 
                    PhotonNetwork.Destroy(gameObject);
                    break;
                }
            case "1upPowerup":
                {
                    PhotonNetwork.Destroy(gameObject);
                    break;
                }
            case "LaserPowerup":
                {
                    PhotonNetwork.Destroy(gameObject);
                    break;
                }
            case "RapidFirePowerup":
                {
                    PhotonNetwork.Destroy(gameObject);
                    break;
                }
            case "TopGunPowerup":
                {
                    PhotonNetwork.Destroy(gameObject);
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
            PhotonNetwork.Destroy(gameObject);
            isOnScreen_m = false;
        }
    }
}