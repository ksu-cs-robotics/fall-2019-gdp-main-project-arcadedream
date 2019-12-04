using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class LapTriggers : MonoBehaviour
    {
        public GameObject[] trigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                string Tname = this.name;

                switch (Tname)
                {
                    case "Trigger1":
                        Debug.Log("Trigger1");
                        trigger[1].SetActive(true);
                        trigger[0].SetActive(false);
                        break;
                    case "Trigger2":
                        Debug.Log("Trigger2");
                        trigger[2].SetActive(true);
                        trigger[1].SetActive(false);
                        break;
                    case "Trigger3":
                        Debug.Log("Trigger3");
                        trigger[3].SetActive(true);
                        trigger[2].SetActive(false);
                        break;
                    case "Trigger4":
                        Debug.Log("Trigger4");
                        trigger[4].SetActive(true);
                        trigger[3].SetActive(false);
                        break;
                    case "Trigger5":
                        Debug.Log("Trigger5");
                        trigger[5].SetActive(true);
                        trigger[4].SetActive(false);
                        break;

                    default:
                        Debug.Log("Lap Trigger Error");
                        break;
                }
            }
        }
    }
}
