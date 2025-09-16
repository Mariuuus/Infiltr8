using System;
using System.Collections;
using System.Numerics;
using __ProjectMain.Scripts.Managers.Ingame;
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
        [SerializeField] private int cooldownTime = 3;
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material transparentMaterial;
        
        private bool _moveToEndPoint = true;
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
            transform.position = (maxMoveStart + maxMoveEnd)/ 2f;
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            if (IngameManager.Instance?.Paused ?? false) return;
            Vector3 moveTo = _moveToEndPoint ?  maxMoveEnd : maxMoveStart;
            Vector3 direction = moveTo - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, moveTo) < 0.1f) _moveToEndPoint = !_moveToEndPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_onCooldown) return;
            
            if (other.CompareTag("Player"))
            {
                GrabController grabController = other.GetComponent<GrabController>();
                StartCoroutine(Cooldown());
                other.GetComponent<PlayerController>().SlowPlayer();

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
            float elapsed = 0f;
            this._meshRenderer.material = transparentMaterial;
            
            while (elapsed < cooldownTime)
            {
                yield return null;
                while (IngameManager.Instance?.Paused ?? false)
                {
                    yield return null;
                }
                elapsed += Time.deltaTime;
            }

            this._meshRenderer.material = normalMaterial;
            _onCooldown = false;
            yield return null;
        }
    }
}
