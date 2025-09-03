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
        private void Start()
        {
            this._meshRenderer = gameObject.GetComponent<MeshRenderer>();
            this._meshRenderer.material = normalMaterial;
        }

        public void Init()
        {
            Debug.Log("emil");
            Vector3 p = transform.position;
            p.x = (maxMoveStart.x + maxMoveEnd.x) / 2f;
            p.z = (maxMoveStart.y + maxMoveEnd.y) / 2f;
            Debug.Log(p.x);
            Debug.Log(p.z);
            transform.position = p;
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(_direction * speed * Time.deltaTime);

            if (!isVertical)
            {
                if (transform.position.x >= maxMoveEnd.x)
                {
                    _direction = Vector3.left;
                }
                else if (transform.position.x <= maxMoveStart.x)
                {
                    _direction = Vector3.right;
                }
            }
            else
            {
                if (transform.position.y >= maxMoveEnd.y)
                {
                    _direction = Vector3.left;
                }
                else if (transform.position.y <= maxMoveStart.y)
                {
                    _direction = Vector3.right;
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
