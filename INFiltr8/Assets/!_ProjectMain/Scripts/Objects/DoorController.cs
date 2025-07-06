using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class DoorController : MonoBehaviour
    {
        public List<HackStatusAmount> requiredHackStatusAmounts = new List<HackStatusAmount>();
        private readonly Dictionary<HackStatus, int> _hackAmounts = new Dictionary<HackStatus, int>();
        private bool _open;
        private TextMeshPro _doorUI;
        private Vector3 _initialPos;

        void Start()
        {
            _hackAmounts.Add(HackStatus.RedHacked, 0);
            _hackAmounts.Add(HackStatus.BlueHacked, 0);
            _hackAmounts.Add(HackStatus.GreenHacked, 0);
            _hackAmounts.Add(HackStatus.YellowHacked, 0);

            _initialPos = transform.position;
            //Debug.Log(_initialPos);
        
            GameObject ui = new GameObject("doorUI");
            // rectTransform to store size, position and anchoring of a gui element
            ui.AddComponent<RectTransform>();
            _doorUI = ui.AddComponent<TextMeshPro>();
        
            ui.transform.position = transform.position + new Vector3(0, 5, 0);
            ui.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 1);
            ui.transform.rotation = quaternion.Euler(-50, 0, 0);
        
            _doorUI.fontSize = 12;
            _doorUI.color = new Color32(255, 255, 255, 255);
            UpdateDoorUI();
        
        }
        public void IncreaseHackStatus(HackStatus hackColor)
        {
            for (int i = 0; i < requiredHackStatusAmounts.Count; i++)
            {
                if (requiredHackStatusAmounts[i].hackStatus == hackColor)
                {
                    Debug.Log("door detected new object");
                    _hackAmounts[hackColor]++;
                    UpdateDoorUI();
                    CheckHackStatus();
                }
            }
        }

        public void DecreaseHackStatus(HackStatus hackColor)
        {
            for (int i = 0; i < requiredHackStatusAmounts.Count; i++)
            {
                if (requiredHackStatusAmounts[i].hackStatus == hackColor)
                {
                    if (_hackAmounts[hackColor] > 0)
                    {
                        Debug.Log("removed object from door");
                        _hackAmounts[hackColor]--;
                        UpdateDoorUI();
                        CheckHackStatus();
                    }
                }
            }
        }

        private void CheckHackStatus()
        {
            int hacked = 0;
        
            foreach (var c in _hackAmounts)
            {
                hacked += c.Value;
            }

            if (hacked >= GetRequiredHackAmount())
            {
                // open door
                StopAllCoroutines();
                transform.position = _initialPos;
                StartCoroutine(MoveDoor(new Vector3(0, -2.01f, 0)));
                _open = true;
            }
            else
            {
                // close door, if it was open previously
                if (_open)
                {
                    StopAllCoroutines();
                    transform.position = _initialPos;
                    _open = false;
                }
            }
        }

        private int GetRequiredHackAmount()
        {
            int res = 0;
        
            foreach(var c in requiredHackStatusAmounts)
            {
                res += c.amount;
            }

            return res;
        }

        private IEnumerator MoveDoor(Vector3 targetPos)
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

        public void UpdateDoorUI()
        {
            if (_doorUI == null) return;
            String uiText = "Required: \n";

            foreach (var color in requiredHackStatusAmounts)
            {
                uiText += " " + color.hackStatus.ToString()[0] + ": " + color.amount;
            }
        
            _doorUI.SetText(uiText);
        }
    
    }
}
