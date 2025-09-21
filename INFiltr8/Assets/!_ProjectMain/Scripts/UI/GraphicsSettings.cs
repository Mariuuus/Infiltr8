using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class GraphicsSettings: MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private TMP_Dropdown resDropdown;
        [SerializeField] private Toggle fullScreen;
        [SerializeField] private Toggle vSync;
        
        
        private Resolution[] _allResolutions;
        private int _selectedResolution ;
        private List<Resolution> _selectedResolutions = new List<Resolution>();
        
        public void Show()
        {
            fullScreen.isOn = Screen.fullScreen;
            vSync.isOn = QualitySettings.vSyncCount != 0;
            
            _allResolutions = Screen.resolutions;
            List<string> resolutions = new List<string>();
            string newRes;
            for (int i = 0; i < _allResolutions.Length; i++)
            {
                var resolution = _allResolutions[i];
                newRes = resolution.width + "x" + resolution.height;
                if (resolution.width == Screen.width && resolution.height == Screen.height)
                {
                    _selectedResolution = i;
                }
                if (!resolutions.Contains(newRes))
                {
                    resolutions.Add(newRes);
                    _selectedResolutions.Add(resolution);
                }
            }
            resDropdown.ClearOptions();
            resDropdown.AddOptions(resolutions);
            resDropdown.value = _selectedResolution;
            resDropdown.RefreshShownValue();
            
                
            container.SetActive(true);
        }
        
        public void Hide()
        {
            container.SetActive(false);
        }

        public void ChangeVSync(bool enable)
        {
            vSync.isOn = enable;
            QualitySettings.vSyncCount = enable ? 1 : 0;
        }
        
        public void ChangeFullScreen(bool enable)
        {
            fullScreen.isOn = enable;
            Screen.fullScreen = enable;
        }

        public void ChangeResolution()
        {
            var value = resDropdown.value;
            _selectedResolution = value;
            Screen.SetResolution(_selectedResolutions[value].width, _selectedResolutions[value].height, Screen.fullScreen);
        }
    }
}