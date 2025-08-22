using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.UI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace __ProjectMain.Scripts.Managers
{
    public enum Device
    {
        Keyboard, Gamepad
    }
    public class ControlsManager : MonoBehaviour
    {
        public static ControlsManager Instance { get; private set; }
        
        public Device usedDevice = Device.Keyboard;
        
        private List<IControlsListener> _listeners;
        
        [SerializeField] private GameObject virtualMouseUI;

        public void AddSubscriber(IControlsListener listener)
        {
            _listeners.Add(listener);
            listener.OnChange();
        }

        public void RemoveSubscriber(IControlsListener listener)
        {
            _listeners.Remove(listener);
        }

        private void SendMessages()
        {
            foreach (var listener in _listeners)
            {
                listener.OnChange();
            }
        }

        public void ActivateVirtualMouse()
        {
            virtualMouseUI.gameObject.SetActive(true);
            //virtualMouseUI.GetComponent<VirtualMouseInput>().enabled = true;
        }
        
        public void DeactivateVirtualMouse()
        {
            virtualMouseUI.gameObject.SetActive(false);
            //virtualMouseUI.GetComponent<VirtualMouseInput>().enabled = false;
        }

        private void Start()
        {
            //DeactivateVirtualMouse();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            _listeners = new List<IControlsListener>();
        }

        public void OnControlsChanged(PlayerInput input)
        {
            Debug.Log("OnControlsChanged: " + input.currentControlScheme);
            var before = usedDevice;
            if (input.currentControlScheme==null) return;
            
            if (input.currentControlScheme.ToLower() == "controller" || input.currentControlScheme.ToLower() == "gamepad")
            {
                usedDevice = Device.Gamepad; 
            }
            else if (input.currentControlScheme.ToLower() == "keyboard&mouse")
            {
                usedDevice = Device.Keyboard;
            }

            if (before != usedDevice)
            {
                SendMessages();
            }
        }
    }
}