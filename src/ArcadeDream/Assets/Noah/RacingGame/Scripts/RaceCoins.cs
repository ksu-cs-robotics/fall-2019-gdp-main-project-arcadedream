using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CarGame
{
    //Placed on each of the coins in the scene
    public class RaceCoins : MonoBehaviour
    {
        public static int coins;
        public AudioSource audioCoin;
        public GameObject particleSystem1;

        private void Start()
        {
            coins = 0;
        }

        private void Update()
        {
            this.gameObject.transform.Rotate(0, 0, 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                coins++;
                Debug.Log("Coins: " + coins);

                //Play coin sound
                audioCoin.Play();

                GameObject.Find("CoinCounter").GetComponent<Text>().text = "Coins: " + coins.ToString();


                //Particle System
                Vector3 coinPos = new Vector3(this.gameObject.GetComponent<Transform>().position.x,
                    this.gameObject.GetComponent<Transform>().position.y, this.gameObject.GetComponent<Transform>().position.z);
                //Debug.Log(coinPos);

                particleSystem1.GetComponent<Transform>().SetPositionAndRotation(coinPos, Quaternion.identity);
                GameObject.Find("ParticleSystemCoin").GetComponent<ParticleSystem>().Play();

                this.gameObject.SetActive(false);
            }
        }

        public void SetCoins()
        {
            GameObject.Find("CoinsText").GetComponent<Text>().text = "Coins: " + coins.ToString();
        }
    }
}