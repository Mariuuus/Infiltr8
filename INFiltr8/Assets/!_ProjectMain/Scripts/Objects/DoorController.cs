using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.UI;
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
        private GameObject _doorUI;
        private Vector3 _initialPos;

        [SerializeField] private GameObject doorUIPrefab;
        [SerializeField] private AudioClip doorOpenSound;


        void Start()
        {
            _hackAmounts.Add(HackStatus.RedHacked, 0);
            _hackAmounts.Add(HackStatus.BlueHacked, 0);
            _hackAmounts.Add(HackStatus.GreenHacked, 0);
            _hackAmounts.Add(HackStatus.YellowHacked, 0);

            _initialPos = transform.position;
            //Debug.Log(_initialPos);

            UpdateDoorUI();
            PrintAmounts();
        }

        public void PrintAmounts()
        {
            Debug.Log("+++++ HACK_AMOUNTS +++++");
            foreach (var keyValuePair in _hackAmounts)
            {
                Debug.Log(keyValuePair.Key + ": " + keyValuePair.Value);
            }
            Debug.Log("+++++ ------------ +++++");
            
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
            PrintAmounts();
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

        public void HackChangeOfObjectInTrigger(HackStatus prevStatus, HackStatus newStatus)
        {
            DecreaseHackStatus(prevStatus);
            IncreaseHackStatus(newStatus);
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
                // play sfx
                SFXManager.instance.PlaySFXClip(doorOpenSound, 1f);
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

            foreach (var c in requiredHackStatusAmounts)
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
                if (transform.position.y > endPos.y)
                {
                    Vector3 currentDoorPos = transform.position;
                    Vector3 smoothPos = Vector3.MoveTowards(currentDoorPos, currentDoorPos + targetPos,
                        5f * Time.fixedDeltaTime);
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
            if (_doorUI == null)
            {
                _doorUI = Instantiate(doorUIPrefab);
                _doorUI.transform.position = transform.position + new Vector3(0, 3, 0);
            } 
            _doorUI.GetComponent<DoorUIController>().UpdateUI(_hackAmounts, requiredHackStatusAmounts);
        }
    }
}