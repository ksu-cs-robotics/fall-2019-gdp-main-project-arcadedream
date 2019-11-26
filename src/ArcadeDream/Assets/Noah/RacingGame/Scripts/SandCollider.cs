using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CarGame
{
    public class SandCollider : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Rigidbody>().drag = 2f;
                Debug.Log("In");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Rigidbody>().drag = 0f;
                Debug.Log("Out");
            }
        }

    }
}
