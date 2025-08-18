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
    
        private int _deviceAmount = 0;
        private TextMeshPro _plateUI;
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
            uIController.UpdateUI(_deviceAmount, deviceLimit);
        }
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                other.GetComponent<HackableDevice>()?.SetController(this.Door);
                
                if (_deviceAmount < deviceLimit)
                {
                    var color = other.GetComponent<HackableDevice>();
                    if(!color.UnHacked) Door.IncreaseHackStatus(color.GetHackColor());    
                }
                _deviceAmount++;
                UpdateUI();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.GetComponent<HackableDevice>()?.ResetController();
            Debug.Log("Leave Trigger");
            if (other.CompareTag("Interactable") && _deviceAmount > 0)
            {
                _deviceAmount--;
                if (_deviceAmount < deviceLimit)
                {
                    var color = other.GetComponent<HackableDevice>();
                    if(!color.UnHacked) Door.DecreaseHackStatus(color.GetHackColor());    
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
