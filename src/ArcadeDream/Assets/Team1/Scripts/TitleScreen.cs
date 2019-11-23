using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PHOTONSHIT
{
    public class TitleScreen : MonoBehaviour
    {
        bool started = false;

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        }

        void Update()
        {
            if (!started)
            {
                //GameObject.Find("Main Camera UI").SetActive(false);
                GameObject.Find("Player").SetActive(false);
                GameObject.Find("MasterUI").SetActive(false);
                GameObject.Find("Jukebox").GetComponent<AudioSource>().enabled = false;

                GameObject.Find("Main Camera").GetComponent<Animation>().Play();
                GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;

                started = true;
            }
        }
    }
}
