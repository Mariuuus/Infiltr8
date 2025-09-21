using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class DialogUI: MonoBehaviour, IControlsListener
    {
        [SerializeField] private Sprite gamepadNextLineIcon;
        [SerializeField] private Sprite gamepadPrevLineIcon;
        [SerializeField] private Sprite keyboardNextLineIcon;
        [SerializeField] private Sprite keyboardPrevLineIcon;
        [SerializeField] private Image buttonNextIcon;
        [SerializeField] private Image buttonPrevIcon;
        
        private void Start()
        {
            ControlsManager.Instance.AddSubscriber(this);
            Debug.Log("is Sub");
        }

        private void OnDestroy()
        {
            ControlsManager.Instance.RemoveSubscriber(this);
            Debug.Log("is no Sub");

        }

        public void OnChange()
        {
            Debug.Log("Changed Controls!");
            switch (ControlsManager.Instance.usedDevice)
            {
                case Device.Gamepad:
                    buttonNextIcon.sprite = gamepadNextLineIcon;
                    buttonPrevIcon.sprite = gamepadPrevLineIcon;
                    break;
                case Device.Keyboard: 
                default:
                    buttonNextIcon.sprite = keyboardNextLineIcon;
                    buttonPrevIcon.sprite = keyboardPrevLineIcon;
                    break;
            }
        }
    }
}