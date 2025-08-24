using System;
using System.Collections;
using System.Numerics;
using __ProjectMain.Scripts.Player;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace __ProjectMain.Scripts.Objects
{
    public class LaserWallController : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float maxMoveDistance = 5f;
        [SerializeField] private int cooldownTime = 3;
        [SerializeField] private bool isVertical = false;
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material transparentMaterial;
        
        private Vector3 _initialPosition;
        private Vector3 _direction = Vector3.right;
        private bool _onCooldown = false;
        private MeshRenderer _meshRenderer;
  
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this._initialPosition = transform.position;
            this._meshRenderer = gameObject.GetComponent<MeshRenderer>();
            this._meshRenderer.material = normalMaterial;
            if (isVertical)
            {
                transform.Rotate(0f, 90f, 0f);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(_direction * speed * Time.deltaTime);

            if (!isVertical)
            {
                if (transform.position.x <= (this._initialPosition.x - this.maxMoveDistance))
                {
                    this._direction = Vector3.right;
                } else if (transform.position.x >= (this._initialPosition.x + this.maxMoveDistance))
                {
                    this._direction = Vector3.left;
                }     
            } else if (isVertical)
            {
                if (transform.position.z <= (this._initialPosition.z - this.maxMoveDistance))
                {
                    this._direction = Vector3.left;
                } else if (transform.position.z >= (this._initialPosition.z + this.maxMoveDistance))
                {
                    this._direction = Vector3.right;
                }     
            }
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_onCooldown) return;
            
            if (other.CompareTag("Player"))
            {
                GrabController grabController = other.GetComponent<GrabController>();
                StartCoroutine(Cooldown());

                if (grabController.IsGrabbing)
                {
                    Debug.Log("player dropped a laptop!");
                    grabController.DropObject(grabController.HeldObj);
                }
          
                Debug.Log("player hit laser wall!");
            }
        }

        IEnumerator Cooldown()
        {
            _onCooldown = true;
            int remainingTime = cooldownTime;
            this._meshRenderer.material = transparentMaterial;
            
            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(1);
                remainingTime--;
            }

            this._meshRenderer.material = normalMaterial;
            _onCooldown = false;
            yield return null;
        }
    }
}
