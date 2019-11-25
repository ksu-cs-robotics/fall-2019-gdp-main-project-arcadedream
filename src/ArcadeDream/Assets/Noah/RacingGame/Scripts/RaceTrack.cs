using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class RaceTrack : MonoBehaviour
    {

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("track");
            if (other.tag == "Player")
            {
                Debug.Log("on track");
            }
        }

    }
}
