using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Implements a basic controller for arcade game cabinets
/// Author: Josh Dotson
/// Version: 1
/// </summary>
[RequireComponent(typeof(InteractController))]
public class GameCabinetController : MonoBehaviour, IInteractable
{
    // Stores the name of the scene
    [SerializeField] public string SUBGAMESCENENAME;

    // private List<(Class that implements the learderboard table)> leaderBoard_m;
    
    // Start is called before the first frame update
    void Start()
    {   
        // leaderBoard_m = new List<()>();

        // Initialize leaderboard list via either some ADSQLConnection class or rather primitively connect using just SqlConnection
        //

        /*GetComponent<InteractController>().OnInteract += (caller) =>
        {
            Play();

            return;
        };*/
    }

    public void Submit()
    {
        // What additive does here is basically just puts the subgame over the old scene, allowing the player to reside in 2 scenes at once
        SceneManager.LoadScene(SUBGAMESCENENAME, LoadSceneMode.Additive);
    }
    public void Cancel()
    {
        return;
    }
}
