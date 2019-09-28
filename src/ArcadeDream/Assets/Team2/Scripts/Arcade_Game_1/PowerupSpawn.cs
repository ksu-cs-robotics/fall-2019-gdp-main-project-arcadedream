using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when an obstacle housing a powerup is destroyed, 
/// this script chooses a random powerup to spawn in its place
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class PowerupSpawn : MonoBehaviour
{
    public GameObject[] powerupArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Bullet" ||
            other.gameObject.tag == "Laser" ||
            other.gameObject.tag == "HomingLaser") ;
        {
            GameObject chosenPowerup = Instantiate(powerupArray[Random.Range(0, powerupArray.Length)],
                                       this.transform.position,
                                       transform.rotation);
        }
    }
}
