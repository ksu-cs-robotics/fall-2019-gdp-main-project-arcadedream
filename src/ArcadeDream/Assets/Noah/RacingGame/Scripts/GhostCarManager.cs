using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class GhostCarManager : MonoBehaviour
    {
        bool isRecording;
        bool dataSaved;
        List<Vector3> recordPositions = new List<Vector3>();
        List<Quaternion> recordRotations = new List<Quaternion>();
        public GameObject player;


        // Start is called before the first frame update
        void Start()
        {
            isRecording = false;
            dataSaved = false;
        }

        private IEnumerator recordReplay()
        {
            while (PlayerCar.isPlaying)
            {
                yield return new WaitForSeconds(0.5f);
                //queue current position
                recordPositions.Add(player.GetComponent<Transform>().position);
                Debug.Log("position" + player.GetComponent<Transform>().position);
                //queue current rotation
                recordRotations.Add(player.GetComponent<Transform>().rotation);
                Debug.Log("record" + player.GetComponent<Transform>().rotation);

            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (PlayerCar.isPlaying && !isRecording)
            {
                StartCoroutine("recordReplay");
                isRecording = true;
            }

            if (!PlayerCar.isPlaying && isRecording && !dataSaved)
            {
                SaveData();
                dataSaved = true;
            }
        }

        void SaveData()
        {
            //pos
        }
    }
}
