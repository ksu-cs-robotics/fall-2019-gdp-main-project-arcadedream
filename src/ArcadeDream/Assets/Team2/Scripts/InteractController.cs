using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements a generic sketelon for interactable objects to handle interactions
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class InteractController : MonoBehaviour
{
    // Assigned via editor references to prefabs
    [SerializeField] public string INTERACTTEXT = "";
    [SerializeField] private GameObject dialogMenuCanvas;

    // Stores a list of players in the current session that interact requests should be ignored
    private List<GameObject> blacklist_m;

    // Event that will be executed when player interacts, and this can be changed at runtime
    public event Func<GameObject, dynamic> OnInteract;

    // Defines default behaviour of the InteractController
    public InteractController()
    {
        blacklist_m = new List<GameObject>();

        Func<GameObject, GameObject> genericInteractHandler = (caller) =>
        {
            return dialogMenuCanvas;
        };

        OnInteract += genericInteractHandler;
    }
    public InteractController(Func<GameObject, GameObject> interactHandler) : this()
    {
        OnInteract += interactHandler;
    }

    public dynamic Interact(GameObject caller)
    {
        // If this caller is muted, dont return anything
        if (blacklist_m.Exists((c) => c == caller))
            throw new Exception("User Refused!"); // return default(GameObject);

        // If everything check out, invoke the assigned interact handler of this instance
        return OnInteract.Invoke(caller);
    }
}