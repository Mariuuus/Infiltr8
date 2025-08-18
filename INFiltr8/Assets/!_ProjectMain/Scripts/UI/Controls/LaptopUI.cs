using System;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class LaptopUI : MonoBehaviour, IControlsListener
    {
        [SerializeField] private GameObject gamepadUI;
        [SerializeField] private GameObject keyboardUI;

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