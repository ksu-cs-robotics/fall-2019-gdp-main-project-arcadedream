using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Behavior for Cancer boss
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public class Cancer : Enemy
{
    protected List<GameObject> players_m;

    protected GameObject victim_m;
    protected bool chargingLaser_m;

    protected bool armsAreOpen;

    [SerializeField] private GameObject rightArm_m;
    [SerializeField] private GameObject leftArm_m;
    [SerializeField] private float ARMTOGGLEINTERVAL = 1.0f;
    protected float armToggleTimer_m;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.CancerBehaviourStandard; // .FlyerStandardBehaviour
        primaryWeapon_m = EnemyWeapons.SingleShot;

        base.Start();

        players_m = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        ChooseVictim(out victim_m);
        armsAreOpen = false;
        armToggleTimer_m = 0.0f;
    }

    protected override void Update()
    {
        armToggleTimer_m += Time.deltaTime;

        // Keep track of when to open and close the arms
        if (armToggleTimer_m >= ARMTOGGLEINTERVAL)
        {
            ToggleBladeArms();
            armToggleTimer_m = 0.0f;
        }

        // Move the boss into position, then do its normal behavior
        if (gameObject.transform.position.x > 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime); // Vector3.MoveTowards(transform.position, destination_m, SPEED * Time.deltaTime);
        }
        else // Once the boss is in position, start the normal behavior pattern     
            base.Update();

        try
        {
            // This exits in a try block so ChooseVictim will be handled when there is no players left
            if (((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m) && IsActive)
            {
                ChooseVictim(out victim_m);
                Shoot();

                weaponTimer_m = 0.0f;
            }
        }
        finally { }
    }
   
    protected override void Shoot()
    {
        // This class does have a weapon (Laser)
        GameObject bullet = PhotonNetwork.Instantiate(PROJECTILE.name, transform.position + Vector3.left, Quaternion.identity);
        

        bullet.transform.LookAt(victim_m.gameObject.transform.position, Vector3.up);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * BULLETSPEED * 100);
    }

    protected void ChooseVictim(out GameObject victim)
    {
        var targets = GameObject.FindGameObjectsWithTag("Player");
        System.Random random = new System.Random();
        victim = targets[random.Next(0, targets.Length)];
    }

    protected void ToggleBladeArms()
    {
        armsAreOpen = !armsAreOpen;
        if (armsAreOpen)
        {
            rightArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, -45, 0);
            leftArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            rightArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            leftArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}