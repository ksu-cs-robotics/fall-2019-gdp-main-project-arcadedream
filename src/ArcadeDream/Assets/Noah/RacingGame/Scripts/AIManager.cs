using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for course select
using UnityEngine.UI;
namespace CarGame
{
    public class AIManager : MonoBehaviour
    {

        public static string clip;

        // for course select
        public GameObject playButton;

        public AudioSource UIselection;

        Animator panelAnim;

        // Start is called before the first frame update
        void Start()
        {
            panelAnim = GameObject.Find("TitlePanels").GetComponent<Animator>();
        }

        public void EasyButton()
        {
            Debug.Log("EASY SELECTED");
            clip = "easy";
            panelAnim.Play("TitlePanels");
            UIselection.Play();
        }

        public void MediumButton()
        {
            Debug.Log("MEDIUM SELECTED");
            clip = "medium";
            panelAnim.Play("TitlePanels");
            UIselection.Play();
        }

        public void HardButton()
        {
            Debug.Log("HARD SELECTED");
            clip = "hard";
            panelAnim.Play("TitlePanels");
            UIselection.Play();
        }

        public void ProButton()
        {
            Debug.Log("PRO SELECTED");
            clip = "pro";
            panelAnim.Play("TitlePanels");
            UIselection.Play();
        }


        //Course button will have course manager script later
        public void DesertCourseButton()
        {
            playButton.SetActive(true);
            UIselection.Play();
        }
    }
}