using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ADTest : MonoBehaviour
{
    private bool canBeRobbed;

    private void Start()
    {
        canBeRobbed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GameCabinet")
        {
            canBeRobbed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "GameCabinet")
        {
            canBeRobbed = true;
        }
    }

    public bool getRobbableStatus()
    {
        return canBeRobbed;
    }
}
