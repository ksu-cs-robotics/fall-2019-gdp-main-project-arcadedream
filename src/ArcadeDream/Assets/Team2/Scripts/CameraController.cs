using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the parent (Camera) gameobject follow another referenced gameobject with a given offset
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject PLAYER;
    [SerializeField] public float SMOOTHING = 0.05f;

    protected Vector3 offset;
    protected Quaternion defaultEulerRotation;

    protected Vector3 interactingOffset;
    protected PlayerController playersController;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - PLAYER.transform.position;
        defaultEulerRotation = transform.rotation;

        interactingOffset = new Vector3(1, 3, 0);
        playersController = PLAYER.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        if (!playersController.IsInteracting)
        {
            transform.position = Vector3.Lerp(transform.position, PLAYER.transform.position + offset, SMOOTHING);
            transform.rotation = defaultEulerRotation;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, PLAYER.transform.position + interactingOffset, SMOOTHING);
            transform.LookAt(playersController.currentInteractableObject_m.transform.position, Vector3.up);
        }      
    }
}
