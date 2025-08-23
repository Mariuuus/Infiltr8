using System;
using __ProjectMain.Scripts.Player;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class LaserWallController : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 direction = Vector3.left;
        public float speed = 2f;
        public float maxMoveDistance = 5f;
  
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.initialPosition = transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(direction * speed * Time.deltaTime);

            if (transform.position.x <= (this.initialPosition.x - this.maxMoveDistance))
            {
                this.direction = Vector3.right;
            } else if (transform.position.x >= (this.initialPosition.x + this.maxMoveDistance))
            {
                this.direction = Vector3.left;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GrabController grabController = other.GetComponent<GrabController>();

                if (grabController.IsGrabbing)
                {
                    Debug.Log("player dropped a laptop!");
                    grabController.DropObject(grabController.HeldObj);
                }
          
                Debug.Log("player hit laser wall!");
            }
        }
    }
}
