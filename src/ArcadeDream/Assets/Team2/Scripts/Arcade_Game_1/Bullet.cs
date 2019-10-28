using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public GameObject Shooter { get; set; }
    private bool isOnScreen_m = false;

    private void Start()
    {
        Shooter = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Shooter.gameObject.tag == other.gameObject.tag || 
            other.gameObject.tag == "Background" ||
            other.gameObject.tag == "Boundary")
            return;

        else if (other.gameObject.tag == "Enemy")
        {
            var healthComponent = other.gameObject.GetComponent<EnemyHealth>();

            healthComponent.TakeDamage(Shooter);
        }

        Destroy(gameObject);
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
