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
            pauseScreen.gameObject.SetActive(true);
        }
        
        public void OnResume()
        {
            IngameManager.Instance.Resume();
            pauseScreen.gameObject.SetActive(false);
        }
    }
}