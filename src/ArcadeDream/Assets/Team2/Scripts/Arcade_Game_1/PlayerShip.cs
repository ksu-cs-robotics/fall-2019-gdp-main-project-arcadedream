using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines various predefined weapon property configurations
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public enum WeaponInfo : int
{
    PlayerDamageStandard = 5,
    PlayerDamageIncreased = 20,

    PlayerFireRateStandard = 5,
    PlayerFireRateIncreased = 10,

    PlayerNumProjectilesStandard = 1,
    PlayerNumProjectilesTriple = 3
}

/// <summary>
/// Instantiates weapon objects
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class Weapon
{
    public double Damage { get; set; }
    public int FireRate { get; set; }
    public int NumProjectiles { get; set; }

    public Weapon()
    {
        Damage = (int)WeaponInfo.PlayerDamageStandard;
        FireRate = (int)WeaponInfo.PlayerFireRateStandard;
        NumProjectiles = (int)WeaponInfo.PlayerNumProjectilesStandard;
    }
    public Weapon(double newDamage) : this()
    {
        Damage = newDamage;
    }
    public Weapon(double newDamage, int newFireRate) : this(newDamage)
    {
        FireRate = newFireRate;
    }
    public Weapon(double newDamage, int newFireRate, int newNumProjectiles) : this(newDamage, newFireRate)
    {
        NumProjectiles = newNumProjectiles;
    }
    public Weapon(Weapon newWeapon) : this(newWeapon.Damage, newWeapon.FireRate, newWeapon.NumProjectiles) { }
}

/// <summary>
/// Defines a statically allocated set of player weapon configurations
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public static class PlayerWeapons
{
    public static Weapon Standard = new Weapon();
    public static Weapon IncreasedDamage = new Weapon((double)WeaponInfo.PlayerDamageIncreased);
    public static Weapon IncreasedFireRate = new Weapon((double)WeaponInfo.PlayerDamageStandard, (int)WeaponInfo.PlayerFireRateIncreased);
    public static Weapon TripleShot = new Weapon((double)WeaponInfo.PlayerDamageStandard, (int)WeaponInfo.PlayerFireRateStandard, (int)WeaponInfo.PlayerNumProjectilesTriple);
}

/// <summary>
/// Implements the basic mechanics and attributes associted with any player's ship
/// Author: Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] public float MOVEMENTSPEED = 1.0f;
    [SerializeField] public float BULLETSPEED = 5.0f;
    [SerializeField] public GameObject BULLETPREFAB;

    // These are serialized for debugging purposes
    [SerializeField] public double HEALTH = 100.0f;
    [SerializeField] public int LIVES = 3;

    // Points will be incremented remotely by impacting Bullets referencing this instance
    public int Points;

    protected Rigidbody rigidbody_m;
    protected Weapon primaryWeapon_m;
    protected float weaponTimer_m;

    // Events that may be useful for others using this class such as Team 1 for UI elements
    public event Action PointsChanged;
    public event Action HealthChanged;
    public event Action Respawned;
    public event Action Death;

    public PlayerShip()
    {
        Points = 0;

        primaryWeapon_m = PlayerWeapons.Standard;

        PointsChanged = () => { };
        HealthChanged = () => { };
        Respawned = () => { };
        Death = () => { };
    }

    #region ** Start, and Update Functions **
    private void Start()
    {
        rigidbody_m = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Attack();
    }
    void FixedUpdate()
    {
        Movement();
    }
    #endregion

    private void Movement()
    {
        var movement = new Vector3();

        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized * MOVEMENTSPEED * Time.deltaTime;

        if (!movement.Equals(Vector3.zero))
        {
            // Start walking animation

            Vector3 newPosition = transform.position + movement;

            rigidbody_m.MovePosition(newPosition);
        }
        else
        {
            // Stop walking animation
        }
    }
    private void Attack()
    {
        weaponTimer_m += Time.deltaTime;

        if ((!Input.GetAxis("Fire1").Equals(0)) && ((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m))
        {
            GameObject bullet = Instantiate(BULLETPREFAB, transform.position + Vector3.right, transform.rotation);
            // make the bullet assinged to the player gameObject
            bullet.GetComponent<Rigidbody>().velocity = Vector3.right * BULLETSPEED;

            weaponTimer_m = 0.0f;
        }

        // We may use this in the future if we decide to add secondarys/abilities
        /*if (!Input.GetAxis("Submit").Equals(0))
        {
            var game = colliders_m.Find((c) => c.gameObject.tag == "Game");

            if (game.Equals(default(Collider)))
            {
                //colliders_m[0].gameObject.GetComponent<>
            }
            else
            {
                //game.gameObject.GetComponent<>
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        // if it is an enemy object ie tagged enemy, destory the player object and decrement the lives
        // TookDamage.Invoke();
        // if it is a powerup, delete it, and apply the effects

        if (other.gameObject.tag != gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "Player":
                {
                    break;
                }
                case "PowerUp":
                {
                    // Get the power ups effects from GetComponent 
                    break;
                }
                default:
                {
                    // DEATH
                    break;
                }
            }
        }
    }
}
