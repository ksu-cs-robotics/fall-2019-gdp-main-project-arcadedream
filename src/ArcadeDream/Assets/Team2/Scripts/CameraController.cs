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
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - PLAYER.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, PLAYER.transform.position + offset, SMOOTHING);
    }
}
