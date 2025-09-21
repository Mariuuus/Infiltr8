using System.Collections;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.UI;


namespace __ProjectMain.Scripts.UI
{
    public enum Setting
    {
        Graphics, Audio
    }
    public class Settings : MonoBehaviour, IClickableMenuElement
    { 
        [SerializeField] private Vector3 normalScale; 
        [SerializeField] private Vector3 hoverScale;
        [SerializeField] private AudioSettings audioSettings;
        [SerializeField] private GraphicsSettings graphicsSettings;

        public GameObject settingsMenu;
        
        private Setting _currentSetting = Setting.Audio;
        
        
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

        public void ChangeToAudioSettings() {
            _currentSetting = Setting.Audio;
            ShowSettingsMenu();
        }
        public void ChangeToGraphicsSettings() {
            _currentSetting = Setting.Graphics;
            ShowSettingsMenu();
        }

        public void ShowSettingsMenu()
        {
            if (_currentSetting == Setting.Audio)
            {
                graphicsSettings.Hide();
                audioSettings.Show();
            }
            else
            {
                graphicsSettings.Show();
                audioSettings.Hide();
            }
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
            GameDataManager.Instance.QuickSave();
        }

        private IEnumerator DelayHide()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            settingsMenu.SetActive(false);
        }
    }
}
