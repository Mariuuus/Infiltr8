using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Player
{
    public class GrabController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private Collider _closestObject;
        private readonly float _grabRange = 5.0f;
        private InputAction _grabAction;
        private InputAction _moveAction;
        private InputAction _colorChangeAction;
        private bool _isGrabbing;

        [SerializeField] 
        private GameObject interactionImage;
    
        [SerializeField] 
        private Transform grabPos;
    
        private GameObject _interactionInstance;
    
        void Start()
        {
            // _grabAction = InputSystem.actions.FindAction("Interact");
            // _moveAction = InputSystem.actions.FindAction("Move");
            // _colorChangeAction = InputSystem.actions.FindAction("colorChange");
        
            //reset rigidbody with the settings here
            if(_closestObject != null)
            {
                var rb = _closestObject.GetComponent<Rigidbody>();
                if (!rb)
                {
                    AddRigidbody();
                }
                else
                {
                    Destroy(rb);
                    AddRigidbody();
                }
            }
        }

        public void OnHack(InputAction.CallbackContext context)
        {
            Debug.LogError("OnHack Input" + context.ToString());
            if (!context.started) return;
            if (!_closestObject) return;
            Debug.Log("Grab");
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, _grabRange);
            
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("grabbable") && !_isGrabbing)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (_closestObject.transform.position - playerPos), out hit, _grabRange))
                    {
                        if (!hit.collider.CompareTag("Wall"))
                        {
                            switch (context.control.name)
                            {
                                case "1": _closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.BlueHacked); break;
                                case "2": _closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.RedHacked); break;
                                case "3": _closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.GreenHacked); break;
                                case "4": _closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.YellowHacked); break;
                            }

                            SetInteractionUI();
                        } 
                        Debug.DrawRay(transform.position, (_closestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                    }
                }
            }
        }
        
        public void OnGrab(InputAction.CallbackContext context)
        {
            Debug.LogError("OnGrab Input" + context.ToString());

            if (!context.started) return;
            if (_closestObject == null) return;
            
            if (!_isGrabbing)
            {
                Vector3 playerPos = transform.position;
                RaycastHit hit;
                if (Physics.Raycast(playerPos, (_closestObject.transform.position - playerPos), out hit, _grabRange))
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        _isGrabbing = false;
                    }
                    else
                    {
                        if (_interactionInstance != null)
                        {
                            Destroy(_interactionInstance); 
                            _interactionInstance = null; 
                        }
                        _isGrabbing = true;
                        var rb = _closestObject.GetComponent<Rigidbody>();
                        _closestObject.transform.parent = grabPos;

                        if (rb)
                        {
                            //Destroy(rb);
                            rb.isKinematic = true;
                            rb.detectCollisions = true;
                        }
                        _closestObject.transform.localPosition = Vector3.zero;
                    }
                }
            } 
            else
            {
                SetInteractionUI();
                _isGrabbing = false;
                _closestObject.transform.parent = null;
                //AddRigidbody();
                var rb = _closestObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
            
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, _grabRange);
        
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("grabbable") && !_isGrabbing)
                {
                    if (_closestObject)
                    {
                        float newDistance = (hitCollider.transform.position - playerPos).magnitude;
                        float oldDistance = (_closestObject.transform.position - playerPos).magnitude;
                    
                        if (newDistance < oldDistance)
                        {
                            _closestObject = hitCollider;
                        }
                    }
                    else
                    {
                        _closestObject = hitCollider;
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (_closestObject.transform.position - playerPos), out hit, _grabRange))
                    {
                        if (!hit.collider.CompareTag("Wall"))
                        {
                            SetInteractionUI();
                        } 
                        Debug.DrawRay(transform.position, (_closestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                    }
                }
            }
        }

        private void AddRigidbody()
        {
            Rigidbody newRigidbody = _closestObject.AddComponent<Rigidbody>();
            newRigidbody.mass = 1f;
            newRigidbody.linearDamping = 0f;
            newRigidbody.angularDamping = 0.05f;
            newRigidbody.useGravity = true;
            newRigidbody.isKinematic = false;
            newRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }

        // void LateUpdate()
        // {
        //     if (_isGrabbing && _closestObject)
        //     {
        //         _closestObject.transform.position = grabPos.position;
        //         _closestObject.transform.rotation = grabPos.rotation;
        //     }
        // }
    
        void SetInteractionUI()
        {
            if (_interactionInstance)
            {
                Destroy(_interactionInstance);    
            }
        
            if (_closestObject == null) return;
        
            _interactionInstance = Instantiate(interactionImage, _closestObject.transform.position + new Vector3(0, 1, 0),
                Quaternion.Euler(40, 0, 0));
        }
    }
}

