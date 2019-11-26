using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame
{
    public class BackGroundMusic : MonoBehaviour
    {

        public AudioSource[] backgroudMusic;
        private int n;

        void Start()
        {
            n = Random.Range(0, 3); //0-2
            backgroudMusic[n].Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (!backgroudMusic[n].isPlaying)
            {
                n++;
                if (n > 2) { n = 0; }//reset n
                backgroudMusic[n].Play();
            }
        }
    }
}
