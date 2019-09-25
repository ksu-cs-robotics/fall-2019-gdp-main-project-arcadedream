using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a list of enemy actions that when combined form a full behaviour cycle
/// Author: Josh Dotson
/// Version: 1
/// Notes: Fully implements IEnumerable
/// </summary>
public class EnemyBehaviour : IEnumerable<EnemyAction>
{
    public List<EnemyAction> standardBehaviourCycle { get; set; }

    public EnemyBehaviour()
    {
        standardBehaviourCycle = new List<EnemyAction>();
    }
    public EnemyBehaviour(List<EnemyAction> newBehaviourCycle) : this()
    {
        standardBehaviourCycle = newBehaviourCycle;
    }
    public EnemyBehaviour(EnemyBehaviour newBehaviourCycle) : this(newBehaviourCycle.standardBehaviourCycle) { }

    #region ** Implementation of IEnumerable **
    public IEnumerator<EnemyAction> GetEnumerator()
    {
        return standardBehaviourCycle.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
}

/// <summary>
/// Defines a basic instruction template that can store enemy behaviour for one particular behaviour cycle period
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class EnemyAction 
{
    // Direction enemy should move, as well as how quickley it should move
    public Vector3 Movement { get; set; }
    // Vector3 velocity;

    // Whether or not an enemy should attack
    public bool IsAttacking;

    public EnemyAction()
    {
        Movement = new Vector3();
        IsAttacking = false;
    }
    public EnemyAction(Vector3 newMovement) : this()
    {
        Movement = newMovement;
    }
    public EnemyAction(Vector3 newMovement, bool isAttacking) : this(newMovement)
    {
        IsAttacking = isAttacking;
    }
    public EnemyAction(EnemyAction copy) : this(copy.Movement, copy.IsAttacking) { }
}

/// <summary>
/// Implements a statically configured list of standard enemy behaviours by enemy type
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public static class XIEnemyBehaviours
{
    public static EnemyBehaviour MoveInSquare = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.forward, true),
            new EnemyAction(Vector3.right, true),
            new EnemyAction(Vector3.back, true),
            new EnemyAction(Vector3.left, true)
        }
    );

    public static EnemyBehaviour FlyerBehaviourStandard = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.forward, false)
        }
    );

    public static EnemyBehaviour AssaulterBehaviourStandard = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.right, true),
            new EnemyAction(Vector3.left, true)
        }
    );

    public static EnemyBehaviour StrikerBehaviourStandard = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.forward, true)
        }
    );

    public static EnemyBehaviour RangerBehaviourStandard = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.forward * 2, false),
            new EnemyAction(Vector3.back, false),
            new EnemyAction(Vector3.back, false)
        }
    );

    // The movement for Cancer is purely conceptual at this point
    public static EnemyBehaviour CancerBehaviourStandard = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(Vector3.zero, false)
            
            /*new EnemyAction
            (
                // First things first, find the player, and set the movement appropriately
                new Func<Vector3>(() =>
                {
                    return new Vector3();
                })(),

                // The thing will not be attacking this interval
                false
            ),
            new EnemyAction
            (
                // In the second interval, attack the player
                new Func<Vector3>(() =>
                {
                    var targets = GameObject.FindGameObjectsWithTag("Player");

                    return new Vector3();
                })(),

                // ATTACK
                true
            ),

            new EnemyAction
            (
                new Func<Vector3>(() =>
                {
                    

                    return new Vector3();
                })(),

                true
            ), */
        }
    );
}

