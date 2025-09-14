using System.Collections;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.UI;


namespace __ProjectMain.Scripts.UI
{
    public class Settings : MonoBehaviour, IClickableMenuElement
    { 
        [SerializeField] private Vector3 normalScale; 
        [SerializeField] private Vector3 hoverScale;

        public GameObject settingsMenu;
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        
        public void Awake()
        {
            settingsMenu.SetActive(false);
        }
        
        public void OnHoverStart() 
        { 
            transform.localScale = hoverScale; 
        }

        public void OnHoverEnd()
        {
            transform.localScale = normalScale;
    
        }

        public void ShowSettingsMenu()
        {
            masterVolumeSlider.value = GameDataManager.Instance.gameData.masterVolume;
            musicVolumeSlider.value = GameDataManager.Instance.gameData.musicVolume;
            sfxVolumeSlider.value = GameDataManager.Instance.gameData.sfxVolume;
            settingsMenu.SetActive(true);
        }
        
        public void OnClick()
        {
            MainMenuManager.Instance.currentState = State.Settings;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.settingsCamera);
            MainMenuManager.Instance.backButtonInMainMenuRef.Show();
            ShowSettingsMenu();
        }

        public void OnUnclick()
        {
            StartCoroutine(DelayHide());
            GameDataUtils.QuickSave(GameDataManager.Instance.gameData);
        }

        private IEnumerator DelayHide()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            settingsMenu.SetActive(false);
        }
    }
}
