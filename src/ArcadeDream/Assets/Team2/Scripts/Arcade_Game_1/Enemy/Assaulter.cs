using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior for Assaulter enemy
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public class Assaulter : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Assaulter's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.MoveInSquare;
        base.Start();
    }
}
