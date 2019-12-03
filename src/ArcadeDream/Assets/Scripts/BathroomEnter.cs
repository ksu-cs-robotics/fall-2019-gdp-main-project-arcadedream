using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomEnter : MonoBehaviour
{
    public GameObject[] walls;
    Animator wallAnim;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("disappear");
            foreach (GameObject wall in walls)
            {
                wallAnim = wall.GetComponent<Animator>();
                wallAnim.Play("WallDisappear");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("reappear");
            foreach (GameObject wall in walls)
            {
                wallAnim = wall.GetComponent<Animator>();
                wallAnim.Play("WallAppear");
            }
        }
    }
}
