using System.Collections;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource1;
        [SerializeField] private AudioSource audioSource2;

        [SerializeField] private AudioClip mainMenuMusicClip;
        [SerializeField] private AudioClip inGameMusicClip;
        
        [SerializeField] private float transitionTime = 1.5f;

        private enum Source { AudioSource1, AudioSource2 }
        private Source _currentSource = Source.AudioSource1;

        public static MusicManager Instance;

        private Coroutine _transitionCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null) return;

            var nextSource = _currentSource == Source.AudioSource1 ? Source.AudioSource2 : Source.AudioSource1;
            var inactiveSource = nextSource == Source.AudioSource1 ? audioSource1 : audioSource2;
            var activeSource = _currentSource == Source.AudioSource1 ? audioSource1 : audioSource2;

            inactiveSource.clip = clip;
            inactiveSource.volume = 0f;
            inactiveSource.Play();

            _currentSource = nextSource;

            if (_transitionCoroutine != null) StopCoroutine(_transitionCoroutine);
            _transitionCoroutine = StartCoroutine(MixSources(activeSource, inactiveSource));
        }

        private IEnumerator MixSources(AudioSource from, AudioSource to)
        {
            float t = 0f;

            while (t < transitionTime)
            {
                t += Time.deltaTime;
                float normalized = t / transitionTime;

                from.volume = Mathf.Lerp(1f, 0f, normalized);
                to.volume = Mathf.Lerp(0f, 1f, normalized);

                yield return null;
            }

            from.Stop();
            to.volume = 1f;
        }

        public void PlayMainMenuMusic() => PlayMusic(mainMenuMusicClip);
        public void PlayInGameMusic() => PlayMusic(inGameMusicClip);
    }
}
