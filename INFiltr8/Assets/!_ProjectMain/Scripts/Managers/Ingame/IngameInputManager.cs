using System;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.Ingame
{
    public class IngameInputManager : MonoBehaviour
    {
        public GameObject pauseScreen;
        public GameObject skipButton;
        
        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            if (IngameManager.Instance.Paused) return;
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

        private void Start()
        {
            if (LevelLoaderManager.Instance.speedrunMode)
            {
                pauseScreen.GetComponent<Button>().interactable = false;
            }

        }

        public void OnSkipLevel()
        {
            if (LevelLoaderManager.Instance.speedrunMode) return;
            GameDataManager.Instance.SwitchToOverview(); 
            GameDataManager.Instance.CompletedLevel(LevelLoaderManager.Instance.levelIndex);
        }
    }
}