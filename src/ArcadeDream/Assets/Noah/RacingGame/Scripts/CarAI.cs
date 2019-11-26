using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//for course select
using UnityEngine.UI;

namespace CarGame
{
    public class CarAI : MonoBehaviour
    {
        bool startedAnim;
        Animator anim;
        public AnimationClip easyAI;
        public AnimationClip mediumAI;
        public AnimationClip hardAI;
        public AnimationClip proAI;
        string clip;


        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            startedAnim = false;
            //EasyButton(); //default easy
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (PlayerCar.isPlaying && !startedAnim)
            {
                if (AIManager.clip == "easy")
                {
                    clip = easyAI.name.ToString();
                }
                if (AIManager.clip == "medium")
                {
                    clip = mediumAI.name.ToString();
                }
                if (AIManager.clip == "hard")
                {
                    clip = hardAI.name.ToString();
                }
                if (AIManager.clip == "pro")
                {
                    clip = proAI.name.ToString();
                }
                anim.Play(clip);
                startedAnim = true;
            }
        }

    }
}