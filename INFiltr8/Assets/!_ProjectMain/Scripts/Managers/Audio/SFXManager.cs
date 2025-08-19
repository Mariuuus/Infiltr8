using UnityEngine;
using LevelLoaderManager = __ProjectMain.Scripts.Managers.Level.LevelLoaderManager;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    
    [SerializeField] private AudioSource sfxObject;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, float volume)
    {
        // GameObject reinspawnen
        AudioSource audioSource = Instantiate(sfxObject, LevelLoaderManager.Instance.playerObject.transform.position, Quaternion.identity);
        
        // AudioClip assignen
        audioSource.clip = audioClip;
        
        // Lautstärke
        audioSource.volume = volume;
        
        // Clip abspielen
        audioSource.Play();
        
        // Länge des Clips ablesen
        float clipLength = audioSource.clip.length;
        
        // Objekt nach Abspielen löschen
        Destroy(audioSource.gameObject, clipLength);
    }
}
