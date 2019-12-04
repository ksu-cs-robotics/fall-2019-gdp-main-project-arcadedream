using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CarGame
{
    public class Cheating : MonoBehaviour
    {
        public GameObject finish;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                RaceCoins.coins++;
                Debug.Log("Coins: " + RaceCoins.coins);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                RaceCoins.coins = RaceCoins.coins + 10;
                Debug.Log("Coins: " + RaceCoins.coins);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameObject.Find("Countdown").GetComponent<RaceCountdown>().minute++;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameObject.Find("Countdown").GetComponent<RaceCountdown>().minute--;
            }

            //Restart level
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }

            //finish active
            if (Input.GetKeyDown(KeyCode.T))
            {
                finish.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerPrefs.SetString("RaceHighScore", "");
            }
        }
    }
}
