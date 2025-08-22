using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Managers.Ingame
{
    public class IngameInputManager : MonoBehaviour
    {
        public GameObject pauseScreen;
        
        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            IngameManager.Instance.Pause();
            if (ControlsManager.Instance.usedDevice == Device.Gamepad)
            {
                ControlsManager.Instance.ActivateVirtualMouse();
            }
            pauseScreen.gameObject.SetActive(true);
        }
        
        public void OnResume()
        {
            IngameManager.Instance.Resume();
            pauseScreen.gameObject.SetActive(false);
            ControlsManager.Instance.DeactivateVirtualMouse();
        }
    }
}