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
        private InputAction _colorChangeAction;

        [Header("Pickup Settings")]
        [SerializeField] private float pickupForce = 150.0f;
        [SerializeField] private float dropDistance = 3.5f;
        
        public bool IsGrabbing {private set; get;}
        public GameObject HeldObj {private set; get;}
        public Rigidbody HeldObjRB {private set; get;}
        
        [SerializeField] private Transform grabPos;
        


        private void Update()
        {
            if (HeldObj != null)
            {
                MoveObject();
            }
        }

        public void MoveObject()
        {
            if (Vector3.Distance(grabPos.position, HeldObj.transform.position) > dropDistance)
            {
                DropObject(HeldObj);
            } else if (Vector3.Distance(grabPos.position, HeldObj.transform.position) > 0.01f)
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
            if (GetComponent<PlayerController>().ClosestObject == null) return;
            
            if (!IsGrabbing)
            {
                Vector3 playerPos = transform.position;
                //Pickup Object
                IsGrabbing = true;
                PickupObject(GetComponent<PlayerController>().ClosestObject .gameObject);
            } 
            else
            {
                GetComponent<PlayerController>().UpdateInteractionUI();
                IsGrabbing = false;
                //Drop
                DropObject(GetComponent<PlayerController>().ClosestObject .gameObject);
            }
            
        }
        
    }
}

