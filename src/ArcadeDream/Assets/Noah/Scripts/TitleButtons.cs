using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CarGame;
namespace CarGame
{
    public class TitleButtons : MonoBehaviour
    {
        public GameObject startMenuUI;
        public GameObject gamePlayUI;
        public GameObject arms;

        public AudioSource buttonSelect;

        public void PlayButton()
        {
            CameraFollow.isPlaying = true;
            //PlayerCar.isPlaying = true;
            gamePlayUI.SetActive(true);
            startMenuUI.SetActive(false);
            arms.SetActive(true);
            buttonSelect.Play();
            Debug.Log("Play");
        }
        public void QuitButton()
        {
            Debug.Log("Quit");
            buttonSelect.Play();
            SceneManager.LoadScene("Main");
            //Application.Quit();

        }

        //retry button in the GameOver screen
        public void RetryButton()
        {
            buttonSelect.Play();
            Debug.Log("Retry");
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
