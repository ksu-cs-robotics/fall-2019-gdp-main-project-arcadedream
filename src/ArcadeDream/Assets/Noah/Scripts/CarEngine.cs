using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    [RequireComponent(typeof(PlayerCar))]
    [RequireComponent(typeof(AudioSource))]
    public class CarEngine : MonoBehaviour
    {
        PlayerCar playerCar;
        AudioSource source;

        [SerializeField] float modifier = 1f;

        void Start()
        {
            playerCar = GetComponent<PlayerCar>();
            source = GetComponent<AudioSource>();
        }

        void Update()
        {
            //Debug.Log(playerCar.Speed);

            float soundPitchDiff = 1;

            if (playerCar.Speed <= 0.001f) { source.volume = 0.2f; }
            if (playerCar.Speed > 0.001f) { soundPitchDiff = 1.1f; source.volume = 0.2f; }
            if (playerCar.Speed > 0.04f) { soundPitchDiff = 1.5f; source.volume = 0.3f; }
            if (playerCar.Speed > 0.06f) { soundPitchDiff = 1.8f; source.volume = 0.4f; }
            if (playerCar.Speed > 0.07f) { soundPitchDiff = 2f; source.volume = 0.5f; }
            if (playerCar.Speed > 0.08f) { soundPitchDiff = 2.5f; source.volume = 0.5f; }

            source.pitch = (playerCar.Speed * 35 / soundPitchDiff) * modifier + 0.6f;

        }
    }
}