﻿using System;
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

    // Whether or not to loop this action infinitely
    public bool Loop;

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
    public EnemyAction(Vector3 newMovement, bool isAttacking, bool loop) : this(newMovement, isAttacking)
    {
        Loop = loop;
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
            new EnemyAction(Vector3.forward, true, false),
            new EnemyAction(Vector3.right, true, false),
            new EnemyAction(Vector3.back, true, false),
            new EnemyAction(Vector3.left, true, false)
        }
    );
    public static EnemyBehaviour FlyAndReturn = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(new Vector3(0, 0, -2), true, false),
            new EnemyAction(new Vector3(1, 0, 0), true, false),
            new EnemyAction(new Vector3(0, 0, 2), true, true)
        }
    );
    public static EnemyBehaviour SwoopDown = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(new Vector3(1, 0, 0), true, false),
            new EnemyAction(new Vector3(1, 0, -1), true, false),
            new EnemyAction(new Vector3(0, 0, -1), true, true)
        }
    );
    public static EnemyBehaviour SwoopUp = new EnemyBehaviour
    (
        new List<EnemyAction>()
        {
            new EnemyAction(new Vector3(-1, 0, 0), true, false),
            new EnemyAction(new Vector3(-1, 0, -1), true, false),
            new EnemyAction(new Vector3(0, 0, -1), true, true)
        }
    );
}

