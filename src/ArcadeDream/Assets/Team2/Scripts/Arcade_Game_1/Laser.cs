using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sets laser speed and movement, cleans up lasers
/// Author: Lew Fortwangler
/// Version: 2
/// </summary>

public class Laser : MonoBehaviour
{
    public GameObject Shooter { get; set; }
    private float laserSpeed_m = 20.0f;
    private bool isOnScreen_m;

    private void Start()
    {
        Shooter = gameObject.transform.parent.gameObject;
        isOnScreen_m = false;
    }

    private Laser()
    {

    }
    public Laser(GameObject playerObject) : this()
    {
        Shooter = playerObject;
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

    private void OnTriggerEnter(Collider other)
    {
        if (Shooter.gameObject.tag == other.gameObject.tag) return;

        switch(other.gameObject.tag)
        {
            case "Player":
                {
                    break;
                }
            case "Enemy":
                {
                    var healthComponent = other.gameObject.GetComponent<EnemyHealth>();

                    healthComponent.TakeDamage();
                    Shooter.GetComponent<PlayerShip>().Points += healthComponent.SCOREVALUE;
                    Destroy(gameObject);

                    break;
                }
        }
    }
}
