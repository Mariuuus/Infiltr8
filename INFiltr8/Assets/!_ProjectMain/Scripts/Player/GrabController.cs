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
        public Collider ClosestObject {private set; get;}
        private readonly float _grabRange = 5.0f;
        private InputAction _grabAction;
        private InputAction _moveAction;
        private InputAction _colorChangeAction;
        public bool IsGrabbing {private set; get;}

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
            if(ClosestObject != null)
            {
                var rb = ClosestObject.GetComponent<Rigidbody>();
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
            if (!context.started) return;
            if (!ClosestObject) return;
            Debug.Log("Grab");
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, _grabRange);
            
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("grabbable") && !IsGrabbing)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, _grabRange))
                    {
                        if (!hit.collider.CompareTag("Wall"))
                        {
                            switch (context.control.name)
                            {
                                case "1": ClosestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.BlueHacked); break;
                                case "2": ClosestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.RedHacked); break;
                                case "3": ClosestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.GreenHacked); break;
                                case "4": ClosestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.YellowHacked); break;
                            }

                            SetInteractionUI();
                        } 
                        Debug.DrawRay(transform.position, (ClosestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                    }
                }
            }
        }
        
        public void OnGrab(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            if (ClosestObject == null) return;
            
            if (!IsGrabbing)
            {
                Vector3 playerPos = transform.position;
                RaycastHit hit;
                if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, _grabRange))
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        IsGrabbing = false;
                    }
                    else
                    {
                        if (_interactionInstance != null)
                        {
                            Destroy(_interactionInstance); 
                            _interactionInstance = null; 
                        }
                        IsGrabbing = true;
                        var rb = ClosestObject.GetComponent<Rigidbody>();
                        ClosestObject.transform.parent = grabPos;

                        if (rb)
                        {
                            //Destroy(rb);
                            rb.isKinematic = true;
                            rb.detectCollisions = true;
                        }
                        ClosestObject.transform.localPosition = Vector3.zero;
                    }
                }
            } 
            else
            {
                SetInteractionUI();
                IsGrabbing = false;
                ClosestObject.transform.parent = null;
                //AddRigidbody();
                var rb = ClosestObject.GetComponent<Rigidbody>();
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
                if (hitCollider.CompareTag("grabbable") && !IsGrabbing)
                {
                    if (ClosestObject)
                    {
                        float newDistance = (hitCollider.transform.position - playerPos).magnitude;
                        float oldDistance = (ClosestObject.transform.position - playerPos).magnitude;
                    
                        if (newDistance < oldDistance)
                        {
                            ClosestObject = hitCollider;
                        }
                    }
                    else
                    {
                        ClosestObject = hitCollider;
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, _grabRange))
                    {
                        if (!hit.collider.CompareTag("Wall"))
                        {
                            SetInteractionUI();
                        } 
                        Debug.DrawRay(transform.position, (ClosestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                    }
                }
            }
        }

        private void AddRigidbody()
        {
            Rigidbody newRigidbody = ClosestObject.AddComponent<Rigidbody>();
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
        
            if (ClosestObject == null) return;
        
            _interactionInstance = Instantiate(interactionImage, ClosestObject.transform.position + new Vector3(0, 1, 0),
                Quaternion.Euler(40, 0, 0));
        }
    }
}

