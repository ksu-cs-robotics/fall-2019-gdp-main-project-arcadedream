using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// Behavior for Flyer enemy
/// Author: Jared Anderson, Josh Dotson
/// Version: 2
/// </summary>
public class Flyer : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.FlyerBehaviourStandard; // .FlyerStandardBehaviour

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    [ClientRpc]
    protected override void RpcShoot() { }// This class does not have a weapon

    // A rough idea of what we talked about with disabling the enemies till on screen
    private void OnBecameVisible() { IsActive = true; }
    private void OnBecameInvisible() { IsActive = false; }
}
