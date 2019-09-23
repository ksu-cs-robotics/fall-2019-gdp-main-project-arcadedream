using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior for Ranger enemy
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public class Ranger : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Ranger's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.FlyAndReturn;
        base.Start();
    }
}
