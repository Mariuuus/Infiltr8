using System.Collections;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class ResetUI: MonoBehaviour, IControlsListener
    {

        [SerializeField] private Sprite gamepadResetIcon;
        [SerializeField] private Sprite keyboardResetIcon;
        [SerializeField] private Image buttonIcon;
        [SerializeField] private GameObject resetDisplay;
        
        private void Start()
        {
            resetDisplay.SetActive(false);
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
                    buttonIcon.sprite = gamepadResetIcon;
                    break;
                case Device.Keyboard: 
                default:
                    buttonIcon.sprite = keyboardResetIcon;
                    break;
            }
            resetDisplay.SetActive(true);
            StartCoroutine(nameof(DisplayAndFade));
        }
        
        IEnumerator DisplayAndFade()
        { 
            resetDisplay.SetActive(true);
            yield return new WaitForSecondsRealtime(5);
            //TODO: fade out (maybe)
            resetDisplay.SetActive(false);
        }
    }
}