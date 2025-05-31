using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ActiviationPlateController : MonoBehaviour
{
    public GameObject activationDoor;

    [SerializeField]
    private int deviceLimit = 0;
    
    private int deviceAmount = 0;
    private TextMeshPro plateUI;
    private DoorController door;
    
    private void Start()
    {
        if (activationDoor != null)
        {
            door = activationDoor.GetComponent<DoorController>();
        }


        GameObject ui = new GameObject("activationPlateUI");
        // rectTransform to store size, position and acnhoring of a gui element
        ui.AddComponent<RectTransform>();
        plateUI = ui.AddComponent<TextMeshPro>();

        ui.transform.position = transform.position + new Vector3(1, 5, 0);
        ui.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 1);
        ui.transform.rotation = quaternion.Euler(-50, 0, 0);
        
        plateUI.fontSize = 12;
        plateUI.color = new Color32(0, 0, 0, 255);
        plateUI.SetText(deviceAmount + " / " + deviceLimit);
    }

    public void UpdateUI()
    {
        plateUI.SetText(deviceAmount + " / " + deviceLimit);
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
                plateUI.color = new Color32(245,27,27,255);
            }
            
            plateUI.SetText(deviceAmount + " / " + deviceLimit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("grabbable") && deviceAmount > 0)
        {
            deviceAmount--;

            if (deviceAmount <= deviceLimit)
            {
                plateUI.color = new Color32(0,0,0,255);
            }

            if (deviceAmount < deviceLimit)
            {
                grabbableType color = other.GetComponent<grabbableType>();
                door.decreaseHackStatus(color.getHackColor());
            }

            plateUI.SetText(deviceAmount + " / " + deviceLimit);
        }
    }
}
