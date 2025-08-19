using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Player;
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

        public Dictionary<HackStatus, int> HacksOnPlate {private set; get;}
        private TextMeshPro _plateUI;
        public DoorController Door { get; private set;}

        [SerializeField] private ActivationPlateUIController uIController;
    
        private void Start()
        {
            if (activationDoor != null)
            {
                Door = activationDoor.GetComponent<DoorController>();
            }

            Door.AddActivationPlate(this);
            
            HacksOnPlate = new Dictionary<HackStatus, int>
            {
                [HackStatus.BlueHacked] = 0,
                [HackStatus.RedHacked] = 0,
                [HackStatus.GreenHacked] = 0,
                [HackStatus.YellowHacked] = 0
            };

            UpdateUI();
        }

        public void ChangeHackStatus(HackStatus remove, HackStatus add)
        {
            HacksOnPlate[remove]--;
            HacksOnPlate[add]++;
        }

        private int GetDevicesOnPlate()
        {
            int sum = 0;
            foreach (var keyPairs in HacksOnPlate)
            {
                sum += keyPairs.Value;
            }

            return sum;
        }
        

        public void UpdateUI()
        {
            uIController.UpdateUI(GetDevicesOnPlate(), deviceLimit);
        }
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                var hackObj = other.GetComponent<HackableDevice>();
                hackObj.SetController(this.Door);

                if (!hackObj.UnHacked)
                {
                    HacksOnPlate[hackObj.GetHackColor()]++;
                    
                    if (GetDevicesOnPlate() <= deviceLimit) Door.RecalculateDoorRequirements();
                }
                
                UpdateUI();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                var hackObj = other.GetComponent<HackableDevice>();
                hackObj.ResetController();

                if (!hackObj.UnHacked)
                {
                    HacksOnPlate[hackObj.GetHackColor()]--;
                    
                    if (GetDevicesOnPlate() <= deviceLimit) Door.RecalculateDoorRequirements();
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
