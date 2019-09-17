﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a list of enemy actions that when combined form a full behaviour cycle
/// Author: Josh Dotson
/// Version: 1
/// Notes: Fully implements IEnumerable, 
/// </summary>
public class EnemyBehaviour : IEnumerable<EnemyAction>
{
    List<EnemyAction> standardBehaviourCycle { get; set; }

    public EnemyBehaviour()
    {
        standardBehaviourCycle = new List<EnemyAction>();
    }
    public EnemyBehaviour(EnemyBehaviour newBehaviourCycle) : this()
    {
        standardBehaviourCycle = newBehaviourCycle.standardBehaviourCycle;
    }

    // The other option is to GetEnumerator in Enemy Update to an IEnumerator class attribute, and call if (MoveNext()) every frame
    /*public IEnumerator<EnemyAction> GetNextCycleAction()
    {
        foreach (var action in standardBehaviourCycle)
        { 
            yield return action;
        }
    }*/

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
    Vector3 Movement { get; set; }
    // Vector3 velocity;

    // Whether or not an enemy should attack
    bool IsAttacking;

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
/// </summary>
public static class XIEnemyBehaviours
{
    // This is for example purposes...
    public static EnemyBehaviour fighterStandard = new EnemyBehaviour()
    {
        // fighter
    };
}

