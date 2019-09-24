using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements enemy spawning
/// Author: Jared Anderson, and to a much lesser extent, Josh Dotson
/// Version: 2
/// </summary>
public class EnemyController : MonoBehaviour
{
    // The enemy prefab to be spawned.
    public GameObject ENEMYPREFAB;

    // The spawn point of this enemy.
    private Transform position_m;

    // Stores the behaviour of the enemy ship, as well as it's iterator
    public EnemyBehaviour Behaviour;
    private IEnumerator<EnemyAction> behaviourIterator_m;

    [SerializeField] public float ENEMYSPEED = 1.0f;

    // Stores the speed of the behaviour cycle in seconds, and keeps track of this in the timer
    [SerializeField] public float BEHAVIOURINTERVAL = 1.0f;
    private float behaviourTimer_m;

    // Start is called before the first frame update
    void Start()
    {
        // Below is where one may initialize a behaviour for this Enemy, and get an iterator for cycling
        Behaviour = XIEnemyBehaviours.MoveInSquare;
        behaviourIterator_m = Behaviour.GetEnumerator();

        // Starts up the iterator
        behaviourIterator_m.MoveNext();

        behaviourTimer_m = 0.0f;

        position_m = GetComponent<Transform>();
        Instantiate(ENEMYPREFAB, position_m.position, position_m.rotation);
    }

    // Update is called every frame
    private void Update()
    {
        // Every interval of the behaviour cycle is tied to the behaviourTimer_m
        behaviourTimer_m += Time.deltaTime;

        if ((behaviourTimer_m >= BEHAVIOURINTERVAL))
        {
            // If there is a new stage in the cycle, start its behaviour
            if (behaviourIterator_m.MoveNext())
            {
                // Reset behaviour timer everytime MoveNext() is successfully called
                behaviourTimer_m = 0.0f;
            }
            else // This else statement resets the cycle from the beginning
            {
                behaviourIterator_m.Reset();
                behaviourIterator_m.MoveNext();
                behaviourTimer_m = 0.0f;
            }
        }

        // Apply the movement commands from the EnemyAction to the actual
        transform.Translate(behaviourIterator_m.Current.Movement * Time.deltaTime * ENEMYSPEED);
    }
}