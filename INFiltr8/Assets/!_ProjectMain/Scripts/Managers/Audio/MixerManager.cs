using UnityEngine;
using UnityEngine.Audio;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class MixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        public void Awake()
        {
            mixer.SetFloat("masterVolume", 0.5f);
            mixer.SetFloat("musicVolume", 0.5f);
        }

        public void setMasterVolume(float level)
        {
            mixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        }
    
        public void setSFXVolume(float level)
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
        }
    
        public void setMusicVolume(float level)
        {
            mixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        }
    }
}
