using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private List<HackStatusAmount> requiredHackStatusAmounts = new List<HackStatusAmount>();
    [SerializeField]
    private List<ActiviationPlateController> PlateControllers = new List<ActiviationPlateController>();
    private Dictionary<HackStatus, int> hackAmounts = new Dictionary<HackStatus, int>();
    private bool open = false;

    void Start()
    {
        hackAmounts.Add(HackStatus.RedHacked, 0);
        hackAmounts.Add(HackStatus.BlueHacked, 0);
        hackAmounts.Add(HackStatus.GreenHacked, 0);
        hackAmounts.Add(HackStatus.YellowHacked, 0);
    }
    public void increaseHackStatus(HackStatus hackColor)
    {
        for (int i = 0; i < requiredHackStatusAmounts.Count; i++)
        {
            if (requiredHackStatusAmounts[i].hackStatus == hackColor)
            {
                if (hackAmounts[hackColor] < requiredHackStatusAmounts[i].amount)
                {
                    Debug.Log("door detected new object");
                    hackAmounts[hackColor]++;
                    checkHackStatus();
                }
            }
        }
    }

    public void decreaseHackStatus(HackStatus hackColor)
    {
        for (int i = 0; i < requiredHackStatusAmounts.Count; i++)
        {
            if (requiredHackStatusAmounts[i].hackStatus == hackColor)
            {
                if (hackAmounts[hackColor] > 0)
                {
                    Debug.Log("removed object from door");
                    hackAmounts[hackColor]--;
                    checkHackStatus();
                }
            }
        }
    }

    private void checkHackStatus()
    {
        int hacked = 0;
        
        foreach (var c in hackAmounts)
        {
            hacked += c.Value;
        }

        if (hacked == getRequiredHackAmount())
        {
            // open door
            moveDoor(-10);
            open = true;
        }
        else
        {
            // close door, if it was open previously
            if (open)
            {
                moveDoor(10);
                open = false;
            }
        }
    }

    private int getRequiredHackAmount()
    {
        int res = 0;
        
        foreach(var c in requiredHackStatusAmounts)
        {
            res += c.amount;
        }

        return res;
    }

    private void moveDoor(int yDirection)
    {
        Vector3 doorPosition = transform.position;
        transform.position = Vector3.Lerp(doorPosition, doorPosition + new Vector3(0, yDirection, 0), 0.3f);
    }
}
