using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CarGame
{
    public class RaceHighScore : MonoBehaviour
    {
        public string[] tierList;
        public Text highscoreText;

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetString("RaceHighScore") != "")
            {
                highscoreText.text = "HIGHSCORE\n" +
                    PlayerPrefs.GetString("RaceHighScore");
            }
        }

        public void SetHighScore(int tierNum)
        {
            string currentHS = PlayerPrefs.GetString("RaceHighScore");
            int i = 0;
            while (i <= tierNum)
            {
                Debug.Log("Checking" + tierList[i]);
                if (tierList[i] != currentHS && i >= FindHighscoreInt(currentHS))
                {
                    PlayerPrefs.SetString("RaceHighScore", tierList[i]);
                    Debug.Log("New highscore: " + tierList[i]);
                    PlayerPrefs.Save();
                }
                i++;
            }
        }

        int FindHighscoreInt(string s)
        {
            int i = 0;
            while (i < tierList.Length)
            {
                if (tierList[i] == s)
                {
                    return i;
                }
                i++;
            }
            return 0;
        }

        string FindHighscoreString(int i)
        {
            return "";
        }
    }
}