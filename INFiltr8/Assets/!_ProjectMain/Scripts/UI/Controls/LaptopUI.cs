using System;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class LaptopUI : MonoBehaviour, IControlsListener
    {
        [SerializeField] private GameObject gamepadUI;
        [SerializeField] private GameObject keyboardUI;
        [SerializeField] private GameObject[] redHackButtons;
        [SerializeField] private GameObject[] blueHackButtons;
        [SerializeField] private GameObject[] greenHackButtons;
        [SerializeField] private GameObject[] yellowHackButtons;
        
        private Transform _followObject;

        public void Init(HackableDevice hackableDevice)
        {
            _followObject = hackableDevice.gameObject.transform;
            Debug.Log("Laptop UI Init");
            foreach (var button in redHackButtons.Concat(blueHackButtons).Concat(greenHackButtons).Concat(yellowHackButtons))
            {
                button.SetActive(false);
            }
            if (hackableDevice == null) return;
            foreach (var hack in hackableDevice.Component.possibleHacks)
            {
                switch (hack)
                {
                    case HackStatus.RedHacked:
                        foreach (var b in redHackButtons)
                        {
                            b.gameObject.SetActive(true);
                        }
                        break;
                    case HackStatus.GreenHacked:
                        foreach (var b in greenHackButtons)
                        {
                            b.gameObject.SetActive(true);
                        }
                        break;
                    case HackStatus.BlueHacked:
                        foreach (var b in blueHackButtons)
                        {
                            b.gameObject.SetActive(true);
                        }
                        break;
                    case HackStatus.YellowHacked:
                        foreach (var b in yellowHackButtons)
                        {
                            b.gameObject.SetActive(true);
                        }
                        break;
                }
            }
        }
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = _followObject.position;

            Vector3 desiredPosition = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            if (Vector3.Distance(currentPosition, desiredPosition) > 0.01f)
            {
                Vector3 newPosition = Vector3.Lerp(currentPosition, desiredPosition, 50 * Time.deltaTime);
                _rigidbody.MovePosition(newPosition);
            }
        }

        private void Start()
        {
            ControlsManager.Instance.AddSubscriber(this);
        }

        private void OnDestroy()
        {
            ControlsManager.Instance.RemoveSubscriber(this);
        }

        public void OnChange()
        {
            switch (ControlsManager.Instance.usedDevice)
            {
                case Device.Gamepad:
                    gamepadUI.SetActive(true);
                    keyboardUI.SetActive(false);
                    break;
                case Device.Keyboard: 
                default:
                    //TODO
                    break;
            }
        }
    }
}