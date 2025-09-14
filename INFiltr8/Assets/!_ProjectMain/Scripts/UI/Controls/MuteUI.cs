using System;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class MuteUI: MonoBehaviour, IControlsListener
    {
        [SerializeField] private Sprite keyboardMuteIcon;
        [SerializeField] private Sprite gamepadMuteIcon;
        [SerializeField] private Image MuteImage;
        private bool isMuted = false;
        
        private void Start()
        {
            ControlsManager.Instance.AddSubscriber(this);
        }

        private void OnDestroy()
        {
            ControlsManager.Instance.RemoveSubscriber(this);
        }

        public void ToggleSound(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            
            if (!isMuted)
            {
                MusicManager.Instance.Mute();
                isMuted = true;
            }
            else
            {
                MusicManager.Instance.Unmute();
                isMuted = false;
            }
        }

        public void OnChange()
        {
            switch (ControlsManager.Instance.usedDevice)
            {
                case Device.Gamepad:
                    MuteImage.sprite = gamepadMuteIcon;
                    break;
                default:
                    MuteImage.sprite = keyboardMuteIcon;
                    break;
            }
        }
    }
}