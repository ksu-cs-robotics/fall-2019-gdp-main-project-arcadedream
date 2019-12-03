//9/2/2019
//Noah Lin
//version 1


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {

        anim = GetComponent<Animator>();
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
