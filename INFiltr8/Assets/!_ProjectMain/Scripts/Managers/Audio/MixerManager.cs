using UnityEngine;
using UnityEngine.Audio;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class MixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        public void Start()
        {
            mixer.SetFloat("masterVolume", Mathf.Log10(GameDataManager.Instance.gameData.masterVolume) * 20f );
            mixer.SetFloat("musicVolume",  Mathf.Log10(GameDataManager.Instance.gameData.musicVolume) * 20f );
            mixer.SetFloat("sfxVolume",  Mathf.Log10(GameDataManager.Instance.gameData.sfxVolume) * 20f );
        }

        public void SetMasterVolume(float level)
        {
            mixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
            GameDataManager.Instance.gameData.masterVolume = level;
        }
    
        public void SetSfxVolume(float level)
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
            GameDataManager.Instance.gameData.sfxVolume = level;

        }
    
        public void SetMusicVolume(float level)
        {
            mixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
            GameDataManager.Instance.gameData.musicVolume = level;

        }
    }
}
