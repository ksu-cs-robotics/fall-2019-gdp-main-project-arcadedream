using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class RacingPositionSystem : MonoBehaviour
    {
        Transform[] checkpoints;
        Transform nextCP;

        float distanceToNextCP;

        float CheckpointScore;

        [HideInInspector] public float positionScore;

        int nextCPIndex;



        void Start()
        {
            checkpoints = GameObject.Find("GameManager").GetComponent<RacePositionManager>().checkpoints;
            nextCP = checkpoints[0];
            positionScore = 100000f;
            nextCPIndex = 0;
        }

        void Update()
        {
            distanceToNextCP = Vector3.Distance(nextCP.position, GetComponent<Transform>().position);

            positionScore = 100000f + CheckpointScore + distanceToNextCP;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Checkpoint" && other.transform.position == nextCP.position)
            {
                CheckpointScore -= 1000f;
                nextCPIndex++;
                nextCP = checkpoints[nextCPIndex];

            }
        }
    }
}
