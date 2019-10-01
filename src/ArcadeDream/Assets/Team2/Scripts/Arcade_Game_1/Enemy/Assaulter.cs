using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior for Assaulter enemy
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public class Assaulter : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.AssaulterBehaviourStandard; // .FlyerStandardBehaviour
        primaryWeapon_m = EnemyWeapons.SingleShot;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // Fire the weapon if the enemy is supposed to attack in this interval; This is in here because it needs to call the derived function
        if (behaviourIterator_m.Current.IsAttacking && ((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m) && IsActive)
        {
            Shoot();
            weaponTimer_m = 0.0f;
        }
    }

    protected override void Shoot()
    {
        // base.Shoot();

        // This class does have a weapon (Bullet)
        GameObject bullet = Instantiate(PROJECTILE, transform.position + Vector3.left, transform.rotation);
        bullet.transform.parent = gameObject.transform;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.left * BULLETSPEED;
        bullet.GetComponent<Bullet>().Shooter = gameObject;
    }
}
