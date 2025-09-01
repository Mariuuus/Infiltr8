using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private MusicLibrary musicLibrary;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        StartCoroutine(MusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }
    
    IEnumerator MusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f,0,percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0,1f,percent);
            yield return null;
        }
    }
    
}
