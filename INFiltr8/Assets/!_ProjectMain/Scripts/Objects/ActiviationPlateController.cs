using System;
using __ProjectMain.Scripts.LevelEditor.Types;
using Unity.VisualScripting;
using UnityEngine;

public class ActiviationPlateController : MonoBehaviour
{
    [SerializeField]
    private GameObject activationDoor;

    private DoorController door;
    private void Start()
    {
        if (activationDoor != null)
        {
            door = activationDoor.GetComponent<DoorController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grabbable"))
        {
            grabbableType color = other.GetComponent<grabbableType>();
            door.increaseHackStatus(color.getHackColor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("grabbable"))
        {
            grabbableType color = other.GetComponent<grabbableType>();
            door.decreaseHackStatus(color.getHackColor());
        }
    }
}
