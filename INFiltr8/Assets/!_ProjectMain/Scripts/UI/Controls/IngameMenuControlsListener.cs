using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.Ingame;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class IngameMenuControlsListener: MonoBehaviour, IControlsListener
    {
        
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
            Debug.Log("OnChange");
            switch (ControlsManager.Instance.usedDevice)
            {
                case Device.Gamepad:
                    if (IngameManager.Instance.Paused) ControlsManager.Instance.ActivateVirtualMouse();
                    break;
                case Device.Keyboard:
                    ControlsManager.Instance.DeactivateVirtualMouse();
                    break;
            }
        }
    }
}