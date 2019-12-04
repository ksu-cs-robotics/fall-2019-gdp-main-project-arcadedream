using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class ActivateLoseUI : MonoBehaviour
    {
        public GameObject loseUI;

        void Start()
        {
            loseUI.SetActive(true);
        }

    }
}