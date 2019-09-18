using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sets laser speed and movement, cleans up lasers
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class Laser : MonoBehaviour
{
    private float laserSpeed_m = 20.0f;
    private bool isOnScreen_m;

    private void Start()
    {
        isOnScreen_m = false;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * laserSpeed_m, 0, 0);
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
