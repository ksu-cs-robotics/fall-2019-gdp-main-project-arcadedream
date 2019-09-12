using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement for obstacle, destruction requirements
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class ObstacleMovement : MonoBehaviour
{
    private float moveSpeed_m = 9.0f;
    private bool isOnScreen_m;  //keep track if the obstacle is visible on screen or not

    private void Start()
    {
        isOnScreen_m = false;
    }

    // Update is called once per frame
    private void Update()
    {   
        transform.Translate(0, 0, -Time.deltaTime * moveSpeed_m);
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
        if(isOnScreen_m == true)
        {
            Destroy(gameObject);
            isOnScreen_m = false;
        }
    }

    //destroy object if collides with the back wall or hits a player ship or bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") Destroy(gameObject);
        if (other.gameObject.tag == "Bullet") Destroy(gameObject);
    }
}
