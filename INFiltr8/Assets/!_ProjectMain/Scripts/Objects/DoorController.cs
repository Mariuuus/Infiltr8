using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Managers.Level;
using __ProjectMain.Scripts.Player;
using __ProjectMain.Scripts.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Objects
{
    public class DoorController : MonoBehaviour, IHighlightByPlayer
    {
        public List<HackStatusAmount> requiredHackStatusAmounts = new List<HackStatusAmount>();
        public bool Open { private set; get; } = false;
        
        private readonly Dictionary<HackStatus, int> _hackAmounts = new Dictionary<HackStatus, int>();
        private GameObject _doorUI;
        private Vector3 _initialPos;

        [SerializeField] private GameObject doorUIPrefab;
        [SerializeField] private AudioClip doorOpenSound;
        [SerializeField] private AudioClip doorCloseSound;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        
        
        private List<ActivationPlateController> _activationPlates;
        
        public void RecalculateDoorRequirements()
        {
            ResetHackRequirements();
            foreach (var plates in _activationPlates)
            {
                foreach (var keyValueP in plates.HacksOnPlate)
                {
                    _hackAmounts[keyValueP.Key] += keyValueP.Value;
                }
            }
            PrintAmounts();
            CheckHackStatus();
            UpdateDoorUI();
        }


        private void ResetHackRequirements()
        {
            _hackAmounts.Clear();
            _hackAmounts.Add(HackStatus.RedHacked, 0);
            _hackAmounts.Add(HackStatus.BlueHacked, 0);
            _hackAmounts.Add(HackStatus.GreenHacked, 0);
            _hackAmounts.Add(HackStatus.YellowHacked, 0);
        }

        public void AddActivationPlate(ActivationPlateController activationPlates)
        {
            Debug.Log("Adding activation plate");
            _activationPlates.Add(activationPlates);
        }


        public void Init()
        {
            _activationPlates = new List<ActivationPlateController>();
            ResetHackRequirements();
            _initialPos = transform.position;
            PrintAmounts();
            UpdateDoorUI();
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

        private void CheckHackStatus()
        {
            bool open = true;
            foreach (var hack in requiredHackStatusAmounts)
            {
                if (_hackAmounts[hack.hackStatus] < hack.amount)
                {
                    open = false;
                }       
            }

            if (open == Open) return;
            if (open)
            {
                //HACKED:
                // open door
                StopAllCoroutines();
                transform.position = _initialPos;
                StartCoroutine(MoveDoor(new Vector3(0, -2.01f, 0)));
                Open = true;
                // play sfx
                SfxManager.instance.PlaySfxClip(doorOpenSound, 1f);
            }
            else
            {
                //UNHACKED:
                // close door, if it was open previously
                if (Open)
                {
                    StopAllCoroutines();
                    transform.position = _initialPos;
                    Open = false;
                    // play sfx
                    SfxManager.instance.PlaySfxClip(doorCloseSound, 1f);
                }
            }
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
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerHighlight();
                LevelLoaderManager.Instance.playerObject.GetComponent<HighlightController>().ChangeHighlightByPlayer(this);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StopHighlightDelayed();
            }
        }
        
        public void TriggerHighlight()
        {
            GetComponent<Renderer>().material = highlightMaterial;
            foreach (var plates in _activationPlates)
            {
                plates.GetComponent<HighlightOnActivation>().TriggerHighlight();
            }
            StopCoroutine(nameof(DissolveHighlight));
        }

        public void StopHighlightDelayed()
        {
            StartCoroutine(nameof(DissolveHighlight));
        }

        public void StopHighlight()
        {
            GetComponent<Renderer>().material = defaultMaterial;
            foreach (var plates in _activationPlates)
            {
                plates.GetComponent<HighlightOnActivation>().StopHighlight();
            }
        }
        
        private IEnumerator DissolveHighlight()
        {
            float duration = 3f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            StopHighlight();
            LevelLoaderManager.Instance.playerObject.GetComponent<HighlightController>().RemoveHighlightReference();
        }
    }
}