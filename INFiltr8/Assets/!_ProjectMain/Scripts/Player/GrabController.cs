using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers.Audio;
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

        [SerializeField] private AudioClip grabSound;
        [SerializeField] private AudioClip dropSound;
        
        public bool IsGrabbing {private set; get;} = false;
        public GameObject HeldObj {private set; get;}
        private Rigidbody HeldObjRb { set; get;}
        
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
                HeldObjRb.AddForce(direction * pickupForce);
            }
        }

        public void PickupObject(GameObject obj)
        {
            if (obj.GetComponent<Rigidbody>())
            {
                HeldObjRb = obj.GetComponent<Rigidbody>();
                HeldObjRb.useGravity = false;
                HeldObjRb.linearDamping = 10;
                HeldObjRb.constraints = RigidbodyConstraints.FreezeRotation;
                
                HeldObjRb.transform.SetParent(grabPos);
                HeldObj = obj;
            }
            Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>(), true);
            GetComponent<PlayerController>().UpdateInteractionUI();
            //play sfx
            SfxManager.instance.PlaySfxClip(grabSound,1f);
            
        }
        
        public void DropObject(GameObject obj)
        {
            HeldObjRb.useGravity = true;
            HeldObjRb.linearDamping = 1;
            HeldObjRb.constraints = RigidbodyConstraints.None;
            
            HeldObjRb.transform.SetParent(null);
            HeldObj = null;
            IsGrabbing = false;
                
            Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>(), false);
            GetComponent<PlayerController>().UpdateInteractionUI();
            //play sfx
            SfxManager.instance.PlaySfxClip(dropSound, 1f);
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

