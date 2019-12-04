using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CarGame
{
    public class PlayerCar : MonoBehaviour
    {
        public float acceleration = 8;
        public float turnSpeed = 5;
        Quaternion targetRotation;
        Rigidbody _rigidbody;
        Vector3 lastPosition;
        public static bool isPlaying;

        float _sideSlipAmount = 0;
        public float sideSlipAmount { get { return _sideSlipAmount; } }
        float _speed;
        public float Speed { get { return _speed; } }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            isPlaying = false;
        }

        private void Update()
        {
            SetRotationPoint();
            SetSideSlip();
        }

        void SetSideSlip()
        {
            Vector3 direction = transform.position - lastPosition;
            Vector3 movement = transform.InverseTransformDirection(direction);
            lastPosition = transform.position;
            _sideSlipAmount = movement.x;

        }

        void SetRotationPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = target - transform.position;
                float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(0, rotationAngle, 0);
            }
        }



        void FixedUpdate()
        {

            if (!isPlaying) return;

            _speed = _rigidbody.velocity.magnitude / 1000;
            float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
            _rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(_speed, -1, 1) * Time.fixedDeltaTime);
        }
    }
}