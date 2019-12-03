using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Behavior for Ranger enemy
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public class Ranger : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.RangerBehaviourStandard; // .FlyerStandardBehaviour

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        transform.Translate(Vector3.forward * (Time.deltaTime / 2));
    }
    
    protected override void Shoot() { }

    // A rough idea of what we talked about with disabling the enemies till on screen
    private void OnBecameVisible() { IsActive = true; }
    private void OnBecameInvisible() { IsActive = false; }
}