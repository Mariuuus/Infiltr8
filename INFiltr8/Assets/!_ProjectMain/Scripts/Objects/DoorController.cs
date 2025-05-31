using System;
using System.Collections;
using Unity.Mathematics;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<HackStatusAmount> requiredHackStatusAmounts = new List<HackStatusAmount>();
    private Dictionary<HackStatus, int> hackAmounts = new Dictionary<HackStatus, int>();
    private bool open = false;
    private TextMeshPro doorUI;
    private Vector3 _initialPos;

    void Start()
    {
        hackAmounts.Add(HackStatus.RedHacked, 0);
        hackAmounts.Add(HackStatus.BlueHacked, 0);
        hackAmounts.Add(HackStatus.GreenHacked, 0);
        hackAmounts.Add(HackStatus.YellowHacked, 0);

        _initialPos = transform.position;
        Debug.Log(_initialPos);
        
        GameObject ui = new GameObject("doorUI");
        // rectTransform to store size, position and acnhoring of a gui element
        ui.AddComponent<RectTransform>();
        doorUI = ui.AddComponent<TextMeshPro>();
        
        ui.transform.position = transform.position + new Vector3(0, 5, 0);
        ui.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 1);
        ui.transform.rotation = quaternion.Euler(-50, 0, 0);
        
        doorUI.fontSize = 12;
        doorUI.color = new Color32(0, 0, 0, 255);
        updateDoorUI();
        
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
                    updateDoorUI();
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
                    updateDoorUI();
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
            StopAllCoroutines();
            transform.position = _initialPos;
            StartCoroutine(moveDoor(new Vector3(0, -2, 0)));
            open = true;
        }
        else
        {
            // close door, if it was open previously
            if (open)
            {
                StopAllCoroutines();
                transform.position = _initialPos;
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

    IEnumerator moveDoor(Vector3 targetPos)
    {

        Vector3 endPos = transform.position + targetPos;

        while (true)
        {
            if (transform.position != endPos)
            {
                Vector3 currentDoorPos = transform.position;
                Vector3 smoothPos = Vector3.MoveTowards(currentDoorPos, currentDoorPos + targetPos, 5f * Time.fixedDeltaTime);
                transform.position = smoothPos;
            }
            else
            {
                yield break;
            }
            
            yield return null;
        }

        // Vector3 doorPosition = transform.position;
        // transform.position = Vector3.Lerp(doorPosition, doorPosition + new Vector3(0, yDirection, 0), 0.3f);
    }

    public void updateDoorUI()
    {
        if (doorUI == null) return;
        String uiText = "Required: \n";

        foreach (var color in requiredHackStatusAmounts)
        {
            uiText += " " + color.hackStatus.ToString()[0] + ": " + color.amount;
        }
        
        doorUI.SetText(uiText);
    }
    
}
