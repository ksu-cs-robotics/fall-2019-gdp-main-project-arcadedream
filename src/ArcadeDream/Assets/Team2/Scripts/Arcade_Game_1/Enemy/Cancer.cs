using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior for Cancer boss
/// Author: Jared Anderson, Josh Dotson
/// Version: 1
/// </summary>
public class Cancer : Enemy
{
    protected List<GameObject> players_m;
    protected Vector3 destination_m;

    protected bool armsAreOpen;

    [SerializeField] private GameObject rightArm_m;
    [SerializeField] private GameObject leftArm_m;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Set the Flyer's movement pattern before running the rest of the standard Enemy Start behavior.
        behaviour_m = XIEnemyBehaviours.CancerBehaviourStandard; // .FlyerStandardBehaviour

        base.Start();

        players_m = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        // For now, cancer just picks the place where are about all the players are
        GetPlayerPositionalAverage(out destination_m);
        armsAreOpen = false;
    }

    protected override void Update()
    {
        behaviourTimer_m += Time.deltaTime;

        // This daughter class completely overrides the base class, as the Update() in base is never called
        if (behaviourTimer_m >= BEHAVIOURINTERVAL)
        {
            GetPlayerPositionalAverage(out destination_m);
            ToggleBladeArms();

            behaviourTimer_m = 0.0f;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination_m, SPEED * Time.deltaTime);
    }

    protected override void Shoot() { } // This class does not have a weapon

    protected void GetPlayerPositionalAverage(out Vector3 input)
    {
        input = Vector3.zero;

        foreach (var player in players_m)
        {
            input += player.gameObject.transform.position / players_m.Count;
        }
    }

    protected void ToggleBladeArms()
    {
        armsAreOpen = !armsAreOpen;
        if (armsAreOpen)
        {
            rightArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, -45, 0);
            leftArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            rightArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            leftArm_m.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
