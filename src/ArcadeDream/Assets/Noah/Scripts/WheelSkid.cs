using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class WheelSkid : MonoBehaviour
    {
        public float intensityModifier = 1.5f;

        Skidmarks skidmarks;
        PlayerCar playerCar;
        int lastSkidPos = -1;
        float SkidIntensity = 0.0f;  //change how often the skidmarks appear
        float ParticleIntensity = 0.001f;  //change how often the skidmarks appear
        ParticleSystem particleSystem1;
        ParticleSystem particleSystem2;

        void Start()
        {
            skidmarks = FindObjectOfType<Skidmarks>();
            //playerCar = GameObject.Find("Car1").GetComponent<PlayerCar>();
            particleSystem1 = GetComponentInChildren<ParticleSystem>();
            particleSystem2 = GameObject.Find("ParticleSystemR").GetComponent<ParticleSystem>();
        }

        void LateUpdate()
        {
            lastSkidPos = skidmarks.AddSkidMark(transform.position, transform.up, intensityModifier, lastSkidPos); //skid no matter what
                                                                                                                   //float intensity = playerCar.sideSlipAmount;
            float intensity = 1f;
            if (intensity < 0) intensity = -intensity; //only positive intensity

            if (intensity > SkidIntensity) //skid only if intense enough
            {
                lastSkidPos = skidmarks.AddSkidMark(transform.position, transform.up, intensityModifier, lastSkidPos);
            }
            else
            {
                lastSkidPos = -1;
            }
            //particle appear on certain intensity
            if (intensity > ParticleIntensity)
            {
                if (particleSystem1 != null && !particleSystem1.isPlaying) particleSystem1.Play();
                //if (particleSystem2 != null && !particleSystem2.isPlaying) particleSystem2.Play();
            }
            else
            {
                if (particleSystem1 != null && particleSystem1.isPlaying) particleSystem1.Stop();
                //if (particleSystem2 != null && particleSystem2.isPlaying) particleSystem2.Stop();
            }
        }
    }
}
