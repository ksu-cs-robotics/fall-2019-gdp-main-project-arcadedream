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
/// Author: Josh Dotson, Lew Fortwangler
/// Version: 2
/// </summary>
public class PlayerShip : MonoBehaviour
{
    [SerializeField] public float MOVEMENTSPEED = 1.0f;
    [SerializeField] public float BULLETSPEED = 5.0f;
    [SerializeField] public GameObject BULLETPREFAB;
    [SerializeField] public GameObject LASERPREFAB;

    // These are serialized for debugging purposes
    [SerializeField] public double HEALTH = 100.0f;
    [SerializeField] public int LIVES = 3;

    private bool isInvulnerable_m;
    private bool isDead_m;
    private float spawnProtectionTimer_m;

    // Points will be incremented remotely by impacting Bullets referencing this instance
    public int Points;

    protected Rigidbody rigidbody_m;
    protected Weapon primaryWeapon_m;
    protected float weaponTimer_m;

    //whether or not the user can fire powerups

    //LASER POWERUP ///////////////////////
    private bool hasLaser_m = false;
    private float startTime_m = 0.0f;
    private float timeCharged_m = 0.0f;
    private float laserWidth_m = 0.25f;
    ///////////////////////////////////////

    //////TOPGUN POWERUP ///////////////////////
    private bool hasTopGun_m = false;
    public GameObject topGun;
    ////////////////////////////////////////////

    //attributes for other powerups in the future

    // Events that may be useful for others using this class such as Team 1 for UI elements
    public event Action PointsChanged;
    public event Action HealthChanged;
    public event Action Respawned;
    public event Action Death;

    #region ** Start, and Update Functions **
    private void Start()
    {
        Points = 0;

        isInvulnerable_m = false;
        isDead_m = false;
        spawnProtectionTimer_m = 0.0f;

        primaryWeapon_m = PlayerWeapons.Standard;

        PointsChanged = () => { };
        HealthChanged = () => { };
        Respawned = () => { };
        Death = () => { };

        topGun.SetActive(false);
        rigidbody_m = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        //calculates how much time has passed while holding down the fire button
        if (Input.GetKeyDown("m"))
        {
            startTime_m = Time.time;
        }
        if (Input.GetKeyUp("m"))
        {
            timeCharged_m = Time.time - startTime_m;
        }

        // Invulnerable is mean't to protect players from insta death upon spawning
        if (isInvulnerable_m)
        {
            spawnProtectionTimer_m -= Time.deltaTime;

            if (spawnProtectionTimer_m <= 0.0f)
            {
                isInvulnerable_m = false;
            }
        }

        Attack();
    }
    void FixedUpdate()
    {
        if (!isDead_m) { Movement(); }
    }
    #endregion

    private void Movement()
    {
        var movement = new Vector3();

        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized * MOVEMENTSPEED * Time.deltaTime;

        if (!movement.Equals(Vector3.zero))
        {
            rigidbody_m.MovePosition(transform.position + movement);
        }
    }
    private void Attack()
    {
        weaponTimer_m += Time.deltaTime;

        //shooting the default weapon
        if ((!Input.GetAxis("Fire1").Equals(0)) && ((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m))
        {
            GameObject bullet = Instantiate(BULLETPREFAB, transform.position + Vector3.right, transform.rotation);
            if(hasTopGun_m == true)
            {
                GameObject topBullet = Instantiate(BULLETPREFAB, topGun.transform.position + Vector3.right, transform.rotation);
                topBullet.GetComponent<Rigidbody>().velocity = Vector3.right * BULLETSPEED;
                topBullet.GetComponent<Bullet>().Shooter = gameObject;
            }

            // make the bullet assinged to the player gameObject
            bullet.GetComponent<Rigidbody>().velocity = Vector3.right * BULLETSPEED;
            bullet.GetComponent<Bullet>().Shooter = gameObject;

            weaponTimer_m = 0.0f;
        }

        //shooting a laser
        if (Input.GetKeyUp("m") && 
            hasLaser_m == true && 
            ((1.0 / primaryWeapon_m.FireRate) <= weaponTimer_m))
        {
            chargeLaser(timeCharged_m);     //calls chargeLaser to modify width based on charged time
            GameObject Laser = Instantiate(LASERPREFAB, transform.position + Vector3.right, LASERPREFAB.transform.rotation);
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
    private IEnumerator Respawn()
    {
        gameObject.transform.position = new Vector3(-100, 0, 0);
        yield return new WaitForSeconds(3);
        
        // Quick and dirty way to set a spawn point for the players
        gameObject.transform.position = new Vector3(-10, 0, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);

        isInvulnerable_m = true;
        spawnProtectionTimer_m = 3.0f;
    }

    private void chargeLaser(float timeCharged_m)
    {
        if ((timeCharged_m >= 1.0) && (timeCharged_m < 2.0))
        {
            laserWidth_m = 0.5f;
        }
        else if ((timeCharged_m >= 2.0) && (timeCharged_m < 3.0))
        {
            laserWidth_m = 1.0f;
        }
        else if (timeCharged_m >= 3.0)
        {
            laserWidth_m = 1.5f;
        }
        else
        {
            laserWidth_m = 0.25f;
        }

        LASERPREFAB.transform.localScale = new Vector3(LASERPREFAB.transform.localScale.x, 
                                                       LASERPREFAB.transform.localScale.y, 
                                                       laserWidth_m);
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
                case "Background":
                {
                    break;
                }
                case "LaserPowerup":
                {
                    hasLaser_m = true;
                    break;
                }
                case "1upPowerup":
                {
                     ++LIVES;
                     break;
                }
                case "TopGunPowerup":
                {
                    topGun.SetActive(true);
                    hasTopGun_m = true;
                    break;
                }
                default:
                {
                    // This line, at least for now, will make sure the neither the player or other players can kill each other
                    if (other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<Bullet>().Shooter.gameObject.tag == "Player")
                        return;

                    // If the player did not just respawn...
                    if (!isInvulnerable_m)
                    {
                        --LIVES;

                        if (LIVES > 0)
                        {
                            StartCoroutine(Respawn());
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            isDead_m = true;

                            // Record score with timestamp here
                        }
                    }

                    break;
                }
            }
        }
    }
}
