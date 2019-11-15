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
    private GameObject newPowerup_m;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
            (other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<Bullet>().Shooter.gameObject.tag != "Enemy") ||
            (other.gameObject.tag == "Laser" && other.gameObject.GetComponent<Laser>()) ||
            other.gameObject.tag == "HomingLaser")
        {
            choosePowerup();
            GameObject chosenPowerup = Instantiate(newPowerup_m, this.transform.position, transform.rotation);
        }
    }

    private void choosePowerup()
    {
        int num_m = Random.Range(1, 10);

        if (num_m == 1)
            newPowerup_m = powerupArray[3];
        if (num_m == 2 || num_m == 3)
            newPowerup_m = powerupArray[1];

        if (num_m > 3)
        {
            int num2_m = Random.Range(1, 3);
            if (num2_m == 1) newPowerup_m = powerupArray[0];
            if (num2_m == 2) newPowerup_m = powerupArray[2];
            if (num2_m == 3) newPowerup_m = powerupArray[4];
        }
    }
}