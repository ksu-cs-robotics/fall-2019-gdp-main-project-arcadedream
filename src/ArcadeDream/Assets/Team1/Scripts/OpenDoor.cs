using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator door;

    private void Start()
    {
        door = GetComponent<Animator>();
        //door.Play("DoorClose");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("door open");
            door.Play("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("door close");
            door.Play("DoorClose");
        }
    }

}
