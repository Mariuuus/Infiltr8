using System;
using System.Collections;
using System.Numerics;
using __ProjectMain.Scripts.Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;

namespace __ProjectMain.Scripts.Objects
{
    public class LaserWallController : MonoBehaviour
    {
        public float speed = 2f;
        public Vector3 maxMoveStart;
        public Vector3 maxMoveEnd;
        public bool isVertical = false;
        [SerializeField] private int cooldownTime = 3;
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material transparentMaterial;
        
       
        private Vector3 _direction = Vector3.right;
        private bool _onCooldown = false;
        private MeshRenderer _meshRenderer;
  
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
        void Start()
        {
            this._meshRenderer = gameObject.GetComponent<MeshRenderer>();
            this._meshRenderer.material = normalMaterial;
            
            Vector3 p = transform.position;
            if (!isVertical)
            {
                p.x = (Math.Max(maxMoveStart.x, maxMoveEnd.x) + Math.Min(maxMoveStart.x, maxMoveEnd.x)) / 2;
                transform.position = p;
            }
            else if (isVertical)
            {
                p.z = (Math.Max(maxMoveStart.z, maxMoveEnd.z) + Math.Min(maxMoveStart.z, maxMoveEnd.z)) / 2;
                transform.position = p;
                transform.Rotate(0f, 90f, 0f);
            }
        }

        public void Init()
        {
            return;
            // calculate center of two points passed placerscript, and set gameobject to the center.
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(_direction * speed * Time.deltaTime);

            if (!isVertical)
            {
                if (transform.position.x <= (transform.position.x - Mathf.Abs(transform.position.x - maxMoveStart.x)))
                {
                    this._direction = Vector3.right;
                } else if (transform.position.x >= (transform.position.x + Mathf.Abs(transform.position.x - maxMoveEnd.x)))
                {
                    this._direction = Vector3.left;
                }     
            } else if (isVertical)
            {
                if (transform.position.z <= (transform.position.y - Mathf.Abs(transform.position.z - this.maxMoveStart.z)))
                {
                    this._direction = Vector3.left;
                } else if (transform.position.z >= (transform.position.y + Mathf.Abs(this.transform.position.z - this.maxMoveEnd.z)))
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
                    var playerObject = grabController.HeldObj;
                    if (playerObject.CompareTag("Interactable"))
                    {
                        playerObject.GetComponent<HackableDevice>().ResetHackStatus();
                        Debug.Log("held laptop got unhacked!");
                    }
                    
                    grabController.DropObject(grabController.HeldObj);
                }
          
                Debug.Log("player hit laser wall!");
            } else if (other.CompareTag("Interactable"))
            {
               other.GetComponent<HackableDevice>().ResetHackStatus();
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
