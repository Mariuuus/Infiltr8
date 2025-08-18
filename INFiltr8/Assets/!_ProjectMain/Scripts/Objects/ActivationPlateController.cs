using __ProjectMain.Scripts.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class ActivationPlateController : MonoBehaviour
    {
        public GameObject activationDoor;

        [SerializeField]
        private int deviceLimit = 0;
    
        private int deviceAmount = 0;
        private TextMeshPro plateUI;
        public DoorController Door { get; private set;}

        [SerializeField] private ActivationPlateUIController uIController;
    
        private void Start()
        {
            if (activationDoor != null)
            {
                Door = activationDoor.GetComponent<DoorController>();
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            uIController.UpdateUI(deviceAmount, deviceLimit);
        }
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("grabbable"))
            {
                other.GetComponent<GrabbableObject>()?.SetController(this.Door);
                
                if (deviceAmount < deviceLimit)
                {
                    GrabbableObject color = other.GetComponent<GrabbableObject>();
                    Door.IncreaseHackStatus(color.getHackColor());    
                }
                deviceAmount++;
                UpdateUI();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.GetComponent<GrabbableObject>()?.ResetController();
            Debug.Log("Leave Trigger");
            if (other.CompareTag("grabbable") && deviceAmount > 0)
            {
                deviceAmount--;
                if (deviceAmount < deviceLimit)
                {
                    GrabbableObject color = other.GetComponent<GrabbableObject>();
                    Door.DecreaseHackStatus(color.getHackColor());
                }
                UpdateUI();
            }
        }

        public void SetMaxDevices(int amount)
        {
            deviceLimit = amount;
        }
    }
}
