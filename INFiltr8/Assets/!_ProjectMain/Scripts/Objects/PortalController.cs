using __ProjectMain.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class PortalController : MonoBehaviour
    {
         private PortalController _otherPortal;
         public bool hasBeenPorted;
         
         private void OnTriggerEnter(Collider other)
         {
             //Debug.Log("Entered portal");
             var playerController = FindFirstObjectByType<PlayerController>();
             var grabController = FindFirstObjectByType<GrabController>();
             if (other.CompareTag("Interactable") && !hasBeenPorted)
                
             {
                 if (grabController.IsGrabbing && playerController.ClosestObject.gameObject == other.gameObject)
                 {
                     grabController.DropObject(grabController.HeldObj);
                 }
                 _otherPortal.hasBeenPorted =  true;
                 other.gameObject.transform.position = _otherPortal.transform.position;
                 other.GetComponent<Rigidbody>().AddForce(_otherPortal.transform.forward * 500, ForceMode.Impulse);
             } 
         }

         private void OnTriggerExit(Collider other)
         {
             //Debug.Log("Exited portal");
             if (other.CompareTag("Interactable"))
             {
                 hasBeenPorted = false;
             }
         }

         public void Init(PortalController portal)
         {
             this._otherPortal = portal;
         }
    }
}