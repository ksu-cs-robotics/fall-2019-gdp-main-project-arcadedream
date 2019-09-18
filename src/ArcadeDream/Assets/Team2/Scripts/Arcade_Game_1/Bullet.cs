﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    GameObject Shooter { get; set; }
    private bool isOnScreen_m = false;

    private void Start()
    {
        Shooter = gameObject.transform.parent.gameObject;
    }

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
