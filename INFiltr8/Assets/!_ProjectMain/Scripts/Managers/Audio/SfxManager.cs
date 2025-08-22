using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;
using LevelLoaderManager = __ProjectMain.Scripts.Managers.Level.LevelLoaderManager;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class SfxManager : MonoBehaviour
    {
        public static SfxManager instance;
    
        [SerializeField] private AudioSource sfxObject;
    
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            } else { Destroy(gameObject); }
        }

        public void PlaySfxClip(AudioClip audioClip, float volume)
        {
            if (audioClip == null) return;

            
            //fallback
            Vector3 position = Vector3.zero;
            
            // wenn SfxManager aktiv in LevelLoader Scene 
            if (LevelLoaderManager.Instance != null)
            {
                position = LevelLoaderManager.Instance.playerObject.transform.position;
            }
            // wenn SfxManager aktiv in LevelSelection Scene (CameraManager und overviewCamera sind vorhanden)
            else if (CameraManager.Instance.overviewCamera != null)
            {
                position = CameraManager.Instance.overviewCamera.transform.position;
            }
            // Debug.Log("Sfx position: " + position);
            
            
            // GameObject reinspawnen
            AudioSource audioSource = Instantiate(sfxObject, position, Quaternion.identity);
        
            // AudioClip assignen
            audioSource.clip = audioClip;
        
            // Lautstärke
            audioSource.volume = volume;
        
            // Clip abspielen
            audioSource.Play();
            Debug.Log("Played Sound: " + audioClip.name);
        
            // Länge des Clips ablesen
            float clipLength = audioSource.clip.length;
        
            // Objekt nach Abspielen löschen
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
