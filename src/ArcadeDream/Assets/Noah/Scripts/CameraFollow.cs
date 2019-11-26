using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform observable;
        public float aheadSpeed;
        public float followDamping;
        public float cameraHeight;
        public static bool isPlaying;

        Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = observable.GetComponent<Rigidbody>();
            isPlaying = false;
        }

        void Update()
        {
            if (!isPlaying) return;
            if (observable == null) return;

            Vector3 targetPosition = observable.position + Vector3.up * cameraHeight + _rigidbody.velocity * aheadSpeed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
        }
    }
}