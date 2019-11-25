using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// Behavior for Striker enemy
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public class Striker : Enemy
{
    GameObject victim_m;
    protected bool chargingLaser_m;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.StrikerBehaviourStandard; // .FlyerStandardBehaviour
        primaryWeapon_m = EnemyWeapons.SingleShot;

        base.Start();

        ChooseVictim(out victim_m);
        chargingLaser_m = false;
    }

    protected override void Update()
    {
        base.Update();
        if (isServer)
        {
            try
            {
                if (behaviourIterator_m.Current.IsAttacking && ((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m) && IsActive)
                {
                    ChooseVictim(out victim_m);
                    RpcShoot();

                    weaponTimer_m = 0.0f;
                }
            }
            finally { }
        }
    }
    [ClientRpc]
    protected override void RpcShoot()
    {
        // base.Shoot();

        // This class does have a weapon (Laser)
        GameObject bullet = Instantiate(PROJECTILE, transform.position + Vector3.left, Quaternion.identity);
        NetworkServer.Spawn(bullet);
        // var targetVelocity = victim_m.GetComponent<Rigidbody>().velocity;
        // var targetDistance = Vector3.Distance(victim_m.transform.position, transform.position);
        // var projectileTravelTime = targetDistance / BULLETSPEED;

        // Vector3 targetingVectorPrediction = victim_m.transform.position + (targetVelocity * projectileTravelTime);

        bullet.transform.LookAt(victim_m.gameObject.transform.position /*targetingVectorPrediction*/ , Vector3.up);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * BULLETSPEED * 100);

        // var rigidbody = bullet.GetComponent<Rigidbody>();
        // rigidbody.rotation = bullet.transform.localRotation;
    }

    protected void ChooseVictim(out GameObject victim)
    {
        var targets = GameObject.FindGameObjectsWithTag("Player");
        System.Random random = new System.Random();
        victim = targets[random.Next(0, targets.Length)];
    }
}