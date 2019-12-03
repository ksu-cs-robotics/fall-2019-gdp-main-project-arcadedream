using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Implements enemy spawning
/// Author: Jared Anderson, and to a much lesser extent, Josh Dotson
/// Version: 2
/// </summary>
public class EnemyController : MonoBehaviourPunCallbacks
{
    // The enemy prefab to be spawned.
     public GameObject ENEMYPREFAB;

    // The spawn point of this enemy.
    private Transform position_m;

    // Start is called before the first frame update
    void Start()
    {
        position_m = GetComponent<Transform>();
        PhotonNetwork.Instantiate(ENEMYPREFAB.name, position_m.position, position_m.rotation);
    }
}