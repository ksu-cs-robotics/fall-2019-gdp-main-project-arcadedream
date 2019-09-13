﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
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
}
