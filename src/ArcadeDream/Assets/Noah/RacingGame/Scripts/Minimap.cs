using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class Minimap : MonoBehaviour
    {
        public Transform player;

        private void LateUpdate()
        {
            Vector3 newPosition = player.position;
            newPosition.y = player.position.y;
            transform.position = newPosition;
        }
    }
}
