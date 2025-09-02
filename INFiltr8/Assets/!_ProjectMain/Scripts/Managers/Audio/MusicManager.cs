using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class MusicManager : MonoBehaviour
    {
        [FormerlySerializedAs("audioSource1")] [SerializeField] private AudioSource inGameSource;
        [FormerlySerializedAs("audioSource2")] [SerializeField] private AudioSource mainMenuMusicSource;
        [FormerlySerializedAs("audioSource2")] [SerializeField] private AudioSource someMusicSource;

        [SerializeField] private AudioClip mainMenuMusicClip;
        [SerializeField] private AudioClip inGameMusicClip;
        
        [SerializeField] private float transitionTime = 1.5f;

        private enum Source { MainMenu, InGame, Some }

        private Source _currentSource = Source.MainMenu;

        public static MusicManager Instance;

        private Coroutine _transitionCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                mainMenuMusicSource.clip = mainMenuMusicClip;
                inGameSource.clip = inGameMusicClip;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// this methode should be called if music should be played that isn't the main menu music or the in game music
        /// Those music should not be reset and start all over, but rather continue.
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySomeMusic(AudioClip clip)
        {
            if (clip == null) return;
            someMusicSource.clip = clip;
            
            SwitchMusicTo(Source.Some);
        }

        private void SwitchMusicTo(Source source)
        {
            if (_currentSource == source)
            {
                var aSource = _currentSource == Source.InGame ? inGameSource : _currentSource == Source.MainMenu ? mainMenuMusicSource : someMusicSource;
                if(!aSource.isPlaying) aSource.Play();
                return;
            }
            var inactiveSource = source == Source.InGame ? inGameSource : source == Source.MainMenu ? mainMenuMusicSource : someMusicSource;
            var activeSource = _currentSource == Source.InGame ? inGameSource : _currentSource == Source.MainMenu ? mainMenuMusicSource : someMusicSource;

            inactiveSource.volume = 0f;
            inactiveSource.Play();
            
            _currentSource = source;
            
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

            from.Pause();
            to.volume = 1f;
        }

        public void PlayMainMenuMusic() => SwitchMusicTo(Source.MainMenu);
        public void PlayInGameMusic() => SwitchMusicTo(Source.InGame);
    }
}
