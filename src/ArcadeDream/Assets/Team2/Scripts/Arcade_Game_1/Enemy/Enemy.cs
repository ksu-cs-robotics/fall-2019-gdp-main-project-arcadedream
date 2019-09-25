using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds various weapon configurations and data about enemy weapon limitations. May be optional
/// Author: Josh Dotson
/// Version: 0.5
/// </summary>
public static class EnemyWeapons
{
    public static Weapon SingleShot = new Weapon((double)WeaponInfo.PlayerDamageStandard, 0.5, 1);
    // public static Weapon Laser = new Weapon((double)WeaponInfo.PlayerDamageIncreased, (int)WeaponInfo.EnemyFireRateSingleShot, 1);
}

/// <summary>
/// Implements the basic enemy mechanics
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    // The speed of this enemy.
    [SerializeField] public float SPEED = 1f;
    [SerializeField] public float BULLETSPEED = 3.0f;

    // Projectile prefab, and weapon config
    [SerializeField] public GameObject PROJECTILE;
    protected Weapon primaryWeapon_m;
    protected float weaponTimer_m;
    
    // Stores the behaviour of the enemy ship, as well as it's iterator
    protected EnemyBehaviour behaviour_m;
    protected IEnumerator<EnemyAction> behaviourIterator_m;

    // Stores the speed of the behaviour cycle in seconds, and keeps track of this in the timer
    [SerializeField] public float BEHAVIOURINTERVAL = 1.0f;
    protected float behaviourTimer_m;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        behaviourIterator_m = behaviour_m.GetEnumerator();

        // Starts up the iterator
        behaviourIterator_m.MoveNext();
        behaviourTimer_m = 0.0f;
    }

    // Update is called every frame
    protected virtual void Update()
    {
        weaponTimer_m += Time.deltaTime;
        behaviourTimer_m += Time.deltaTime;

        if (behaviourTimer_m >= BEHAVIOURINTERVAL) // && behaviourIterator_m.Current.Loop == false)
        {
            if (behaviourIterator_m.MoveNext())
            {
                behaviourTimer_m = 0.0f;
            }
            else // This else statement resets the cycle from the beginning
            {
                behaviourIterator_m.Reset();
                behaviourIterator_m.MoveNext();
                behaviourTimer_m = 0.0f;
            }
        }

        transform.Translate(behaviourIterator_m.Current.Movement * Time.deltaTime * SPEED);
    }

    // Will handle all the speciic things that have to do with a particular enemies weapon capabilities
    protected virtual void Shoot() { }
}