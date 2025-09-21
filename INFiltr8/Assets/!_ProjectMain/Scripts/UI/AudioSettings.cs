using __ProjectMain.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class AudioSettings: MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        public void Show()
        {
            container.SetActive(true);
            masterVolumeSlider.value = GameDataManager.Instance.gameData.masterVolume;
            musicVolumeSlider.value = GameDataManager.Instance.gameData.musicVolume;
            sfxVolumeSlider.value = GameDataManager.Instance.gameData.sfxVolume;
        }
        
                
        public void Hide()
        {
            container.SetActive(false);
        }
    }
}