using __ProjectMain.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class PortalController : MonoBehaviour
    {
         private PortalController otherPortal;
         public bool hasBeenPorted;
         
         private void OnTriggerEnter(Collider other)
         {
             Debug.Log("Entered portal");
             var grabController = FindFirstObjectByType<GrabController>();
             if (other.CompareTag("grabbable") && !hasBeenPorted &&
                 !(grabController.IsGrabbing && grabController.ClosestObject.gameObject == other.gameObject))
             {
                 otherPortal.hasBeenPorted =  true;
                 other.gameObject.transform.position = otherPortal.transform.position;
                 other.GetComponent<Rigidbody>().AddForce(otherPortal.transform.forward * 500, ForceMode.Impulse);
             } 
         }

         private void OnTriggerExit(Collider other)
         {
             Debug.Log("Exited portal");
             if (other.CompareTag("grabbable"))
             {
                 hasBeenPorted = false;
             }
         }

         public void Init(PortalController portal)
         {
             this.otherPortal = portal;
         }
    }
}