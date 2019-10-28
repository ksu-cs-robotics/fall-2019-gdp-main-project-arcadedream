using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Homing laser functionality, moves directly toward an enemy
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class HomingMovement : MonoBehaviour
{
    public GameObject Shooter { get; set; }
    private float laserSpeed_m = 10.0f;

    private void Start()
    {
        Shooter = gameObject.transform.parent.gameObject;
    }

    private HomingMovement()
    {

    }
    public HomingMovement(GameObject playerObject) : this()
    {
        Shooter = playerObject;
    }

    private void Update()
    {
        FindEnemy();
    }

    private void FindEnemy()
    {
        float distClosestEnemy_m = Mathf.Infinity;
        Enemy closestEnemy_m = null;
        Enemy[] Enemies_m = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy currentEnemy_m in Enemies_m)
        {
            float distToEnemy_m = (currentEnemy_m.transform.position - this.transform.position).sqrMagnitude;
            if(distToEnemy_m < distClosestEnemy_m)
            {
                distClosestEnemy_m = distToEnemy_m;
                closestEnemy_m = currentEnemy_m;
            }
            transform.position = Vector3.MoveTowards(transform.position,
                                                     currentEnemy_m.transform.position,
                                                     laserSpeed_m * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    break;
                }
            case "Enemy":
                {
                    Destroy(gameObject);
                    var healthComponent = other.gameObject.GetComponent<EnemyHealth>();
                    healthComponent.TakeDamage(Shooter);

                    break;
                }
        }
    }
}
