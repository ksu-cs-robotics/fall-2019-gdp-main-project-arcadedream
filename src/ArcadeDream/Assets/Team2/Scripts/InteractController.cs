using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines basic operations that all Interactable objects must implement
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public interface IInteractable
{
    void Submit();
    void Cancel();
}

/// <summary>
/// Implements a generic sketelon for interactable objects to handle interactions
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class InteractController : MonoBehaviour
{
    // Assigned via editor references to prefabs
    [SerializeField] public string INTERACTTEXT = "";
    [SerializeField] private GameObject UIGameObject;

    // Stores a list of players in the current session that interact requests should be ignored
    private List<GameObject> blacklist_m;

    // Event that will be executed when player interacts, and this can be changed at runtime
    public event Action OnInteract;

    // Defines default behaviour of the InteractController
    public InteractController()
    {
        blacklist_m = new List<GameObject>();

        // Initialize default behaviour
        Action genericInteractHandler = () =>
        {
            // Toggle whether or not UIGameObject is enabled
            UIGameObject.SetActive(!UIGameObject.activeSelf);
        };

        OnInteract += genericInteractHandler;
    }
    public InteractController(Action interactHandler) : this()
    {
        OnInteract += interactHandler;
    }

    public void Interact(GameObject caller)
    {
        // If this caller is muted, dont return anything
        if (blacklist_m.Exists((c) => c == caller))
            throw new Exception("User Refused!"); // return default(GameObject);

        // If everything check out, invoke the assigned interact handler of this instance
        OnInteract.Invoke();
    }
}