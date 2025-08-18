using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Player
{
    public class GrabController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public Collider ClosestObject {private set; get;}
        private InputAction _colorChangeAction;
        
        [Header("Pickup Settings")]
        [SerializeField] private float pickupRange = 3.0f;
        [SerializeField] private float pickupForce = 150.0f;
        
        public bool IsGrabbing {private set; get;}
        public GameObject HeldObj {private set; get;}
        public Rigidbody HeldObjRB {private set; get;}
        
        [SerializeField] private Transform grabPos;
        [SerializeField] private GameObject interactionImage;
        
        private GameObject _interactionInstance;

        public void OnHack(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            if (!ClosestObject) return;
            Debug.Log("Grab");
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, pickupRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("grabbable") && !IsGrabbing)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, pickupRange))
                    {
                        if (!hit.collider.CompareTag("Wall"))
                        {
                            switch (context.control.name)
                            {
                                case "1": ClosestObject.GetComponent<GrabbableObject>().changeMaterial(HackStatus.BlueHacked); break;
                                case "2": ClosestObject.GetComponent<GrabbableObject>().changeMaterial(HackStatus.RedHacked); break;
                                case "3": ClosestObject.GetComponent<GrabbableObject>().changeMaterial(HackStatus.GreenHacked); break;
                                case "4": ClosestObject.GetComponent<GrabbableObject>().changeMaterial(HackStatus.YellowHacked); break;
                            }

                            SetInteractionUI();
                        } 
                        Debug.DrawRay(transform.position, (ClosestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                    }
                }
            }
        }

        private void Update()
        {
            if (HeldObj != null)
            {
                MoveObject();
            }
        }

        public void MoveObject()
        {
            if (Vector3.Distance(grabPos.position, HeldObj.transform.position) > 0.01f)
            {
                Vector3 direction = grabPos.position - HeldObj.transform.position;
                HeldObjRB.AddForce(direction * pickupForce);
            }
        }

        public void PickupObject(GameObject obj)
        {
            if (obj.GetComponent<Rigidbody>())
            {
                HeldObjRB = obj.GetComponent<Rigidbody>();
                HeldObjRB.useGravity = false;
                HeldObjRB.linearDamping = 10;
                HeldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
                
                HeldObjRB.transform.SetParent(grabPos);
                HeldObj = obj;
            }
        }
        
        public void DropObject(GameObject obj)
        {
            HeldObjRB.useGravity = true;
            HeldObjRB.linearDamping = 1;
            HeldObjRB.constraints = RigidbodyConstraints.None;
            
            HeldObjRB.transform.SetParent(null);
            HeldObj = null;
            IsGrabbing = false;
        }
        
        public void OnGrab(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            if (ClosestObject == null) return;
            
            if (!IsGrabbing)
            {
                Vector3 playerPos = transform.position;
                RaycastHit hit;
                if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, pickupRange))
                {
                    if (!hit.collider.CompareTag("Wall"))
                    {
                        // Remove Hack and Grab UI
                        if (_interactionInstance != null)
                        {
                            Destroy(_interactionInstance); 
                            _interactionInstance = null; 
                        }
                        
                        //Pickup Object
                        IsGrabbing = true;
                        PickupObject(ClosestObject.gameObject);
                    }
                }
            } 
            else
            {
                SetInteractionUI();
                IsGrabbing = false;
                //Drop
                DropObject(ClosestObject.gameObject);
            }
            
        }
        
        void FixedUpdate()
        {
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, pickupRange);
        
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
                    if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit, pickupRange))
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

