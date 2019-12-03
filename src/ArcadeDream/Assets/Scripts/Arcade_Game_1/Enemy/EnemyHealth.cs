using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements enemy health and damage functions
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    // The amount of health the enemy starts the game with.
    public int STARTINGHEALTH = 1;
    // The amount added to the player's score when the enemy dies.
    public int SCOREVALUE = 100;
    //The sound to play when the enemy dies.
    public AudioClip DEATHCLIP;
    // Amount of time before enemy object is destroyed.
    public float DEATHDURATION = 2f;

    // Whether the enemy is dead.
    private bool isDead_m;
    // The amount of health the enemy currently has.
    private int currentHealth_m;

    private AudioSource audioSource_m;

    /// <summary>
    /// Added by team3 to allow for death sound effect while still destroying the model
    /// </summary>
    public MeshRenderer model;
    private Collider hb;
    

    void Awake()
    {
        // Initialize bool.
        isDead_m = false;
        // Spawn enemy at full health.
        currentHealth_m = STARTINGHEALTH;

        // Get references.
        audioSource_m = GetComponent<AudioSource>();
        hb = GetComponent<Collider>();
    }

    public void TakeDamage(GameObject Shooter)
    {
        // If the enemy is dead, exit the function.
        if (isDead_m)
            return;

     //   audioSource_m.Play();
          currentHealth_m--;

        if (currentHealth_m <= 0)
        {
            Shooter.GetComponent<PlayerShip>().Points += SCOREVALUE;
            Die();
        }
    }

    void Die()
    {
        isDead_m = true;

        // Set the audio source to the death clip and play it.
        audioSource_m.clip = DEATHCLIP;
        audioSource_m.Play();

        model.enabled = false;
        hb.enabled = false;
        this.enabled = false;
        
        // Increase the player's score by the enemy's score value.
        // TODO: Implement score manager.
        // After the given amount of time, destroy the enemy object.
        Destroy(gameObject, DEATHDURATION);
    }
}
