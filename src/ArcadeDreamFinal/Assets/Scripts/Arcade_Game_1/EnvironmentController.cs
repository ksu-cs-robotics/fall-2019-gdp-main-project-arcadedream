using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for scrolling background and spawning obstacles
/// Will randomly select powerup to spawn in place when power up obstacle is destroyed
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class EnvironmentController : MonoBehaviour
{
    private float timer_m = 3;
    private float x_m, y_m, z_m;
    private Vector3 spawnPoint_m;
    private Vector3 position_m;

    public GameObject POWERUPOBSTACLE_PREFAB;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    
    //spawns obstacles once every 3 seconds, can adjust spawn rate as necessary
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(timer_m);
        spawnPoint_m = getSpawnVector();
        //randomly select which obstacle is going to spawn in future versions, for now just "PowerupObstacle"
        GameObject PowerupObstacle = Instantiate(POWERUPOBSTACLE_PREFAB, spawnPoint_m, transform.rotation);
        StartCoroutine(SpawnRoutine());
    }

    //randomly creates a vector to spawn an obstacle in within some bounds
    private Vector3 getSpawnVector() 
    {
        x_m = 15;
        y_m = 0;
        z_m = Random.Range(-4, 4);
        position_m = new Vector3(x_m, y_m, z_m);
        return position_m;
    }
}
