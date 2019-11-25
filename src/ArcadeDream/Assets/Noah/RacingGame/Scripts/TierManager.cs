using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CarGame
{
    public class TierManager : MonoBehaviour
    {
        public GameObject tierNameObj;
        public GameObject coinsObj;
        public GameObject timeObj;

        [System.Serializable]
        public class Tier
        {
            public string _tierName;
            public int _coinNumber;
            public int _minutes;
            public float _seconds;
        }

        public Tier[] tiers;

        public string tierAwarded;

        public GameObject countdownObj;

        void Start()
        {
            for (int i = 0; i < tiers.Length; ++i)
            {

                SetTier(i);
                SetCoin(i);
                SetTime(i);

                //Achieved Tier

                SetAchievedTier(i);
            }

            //
            if (PlayerPrefs.GetString("RaceHighScore") == "")
            {
                PlayerPrefs.SetString("RaceHighScore", "SH*T TIER");
                PlayerPrefs.Save();
            }
        }

        //
        void SetAchievedTier(int i)
        {
            //if coins >= tier coins 
            // if minutes <= tier minutes
            // if laptime <= tier seconds
            //Set tier achieved text
            if (RaceCoins.coins >= tiers[i]._coinNumber)
            {
                if (countdownObj.GetComponent<RaceCountdown>().minute < tiers[i]._minutes)
                {
                    GameObject.Find("AchievedTierPanel").GetComponentInChildren<Text>().text = tiers[i]._tierName;
                    Debug.Log("Set to: " + tiers[i]._tierName);
                    GameObject.Find("GameManager").GetComponent<RaceHighScore>().SetHighScore(i);
                }
                else if (countdownObj.GetComponent<RaceCountdown>().minute == tiers[i]._minutes)
                {
                    if (countdownObj.GetComponent<RaceCountdown>().laptime <= tiers[i]._seconds)
                    {
                        GameObject.Find("AchievedTierPanel").GetComponentInChildren<Text>().text = tiers[i]._tierName;
                        Debug.Log("Set to: " + tiers[i]._tierName);
                        GameObject.Find("GameManager").GetComponent<RaceHighScore>().SetHighScore(i);
                    }
                }
            }
        }

        void SetTier(int i)
        {
            //Set Tier Name
            Instantiate(tierNameObj); //Create object
                                      //Find the newly created object
            GameObject tierName = GameObject.Find("TierName(Clone)");
            //Set the parent
            tierName.transform.SetParent(GameObject.Find("TierNameColumn").transform);
            //Set Image color of every other item
            if (i % 2 == 0)
            {
                tierName.GetComponent<Image>().color = Color.gray;
            }
            //Set text from tiers
            tierName.GetComponentInChildren<Text>().text = tiers[i]._tierName;
            //Must rename or there will be problems with refinding newly created object
            tierName.name = "TierName" + i.ToString();
        }

        void SetCoin(int i)
        {
            //Set Tier Name
            Instantiate(coinsObj); //Create object
                                   //Find the newly created object
            GameObject coins = GameObject.Find("Coins(Clone)");
            //Set the parent
            coins.transform.SetParent(GameObject.Find("CoinAmountColumn").transform);
            //Set Image color of every other item
            if (i % 2 == 0)
            {
                coins.GetComponent<Image>().color = Color.gray;
            }
            //Set text from tiers
            coins.GetComponentInChildren<Text>().text = tiers[i]._coinNumber.ToString();
            //Must rename or there will be problems with refinding newly created object
            coins.name = "Coins" + i.ToString();
        }

        void SetTime(int i)
        {
            //Set Tier Name
            Instantiate(timeObj); //Create object
                                  //Find the newly created object
            GameObject time = GameObject.Find("Time(Clone)");
            //Set the parent
            time.transform.SetParent(GameObject.Find("TimeColumn").transform);
            //Set Image color of every other item
            if (i % 2 == 0)
            {
                time.GetComponent<Image>().color = Color.gray;
            }
            //Set text from tiers
            time.GetComponentInChildren<Text>().text = tiers[i]._minutes.ToString() + ":" + tiers[i]._seconds.ToString("00");
            //Must rename or there will be problems with refinding newly created object
            time.name = "Time" + i.ToString();
        }
    }
}