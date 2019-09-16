using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements the basic enemy mechanics
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    // The speed of this enemy.
    public float SPEED = 1f;

    public abstract void Attack();
    public abstract void Move();
}