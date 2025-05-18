using System;
using __ProjectMain.Scripts.LevelEditor.Types;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ActiviationPlateController : MonoBehaviour
{
    [SerializeField]
    private GameObject activationDoor;

    [SerializeField]
    private int deviceLimit = 0;

    [SerializeField]
    private TMP_Text limitText;

    private int deviceAmount = 0;
    private TMP_Text _limitTextInstance;
    private DoorController door;
    
    private void Start()
    {
        if (activationDoor != null)
        {
            door = activationDoor.GetComponent<DoorController>();
        }

        _limitTextInstance = Instantiate(limitText, transform.position + new Vector3(1, 5, 0), quaternion.Euler(-50, 0, 0));
        _limitTextInstance.SetText("0 / " + deviceLimit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grabbable"))
        {
            if (deviceAmount < deviceLimit)
            {
                grabbableType color = other.GetComponent<grabbableType>();
                door.increaseHackStatus(color.getHackColor());    
            }
            
            deviceAmount++;

            if (deviceAmount > deviceLimit)
            {
                _limitTextInstance.color = new Color32(245,27,27,255);
            }
            _limitTextInstance.SetText(deviceAmount + " / " + deviceLimit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("grabbable") &&  deviceAmount > 0)
        {
            deviceAmount--;

            if (deviceAmount <= deviceLimit)
            {
                _limitTextInstance.color = new Color32(0,0,0,255);
            }
            
            _limitTextInstance.SetText(deviceAmount + " / " + deviceLimit);
            grabbableType color = other.GetComponent<grabbableType>();
            door.decreaseHackStatus(color.getHackColor());
        }
    }
}
