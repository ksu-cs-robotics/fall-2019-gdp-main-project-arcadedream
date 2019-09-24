using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Store info about the two interacting parties, as well as, define certain generic operations that may happen between them
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class InteractControllerUI : MonoBehaviour, IInteractable
{
    public GameObject InteractableObject { get; set; }
    public GameObject InteractingObject { get; set; }

    #region ** Implementation of IInteractable **
    public void Submit()
    {
        InteractableObject.GetComponent<IInteractable>().Submit();
    }
    public void Cancel()
    {
        Destroy(this.gameObject);

        // InteractableObject.GetComponent<IInteractable>().Cancel();
    }
    #endregion
}
