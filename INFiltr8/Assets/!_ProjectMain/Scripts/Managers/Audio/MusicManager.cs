using System;
using System.Collections;
using __ProjectMain.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace __ProjectMain.Scripts.Managers.Audio
{
    public class MusicManager : MonoBehaviour
    {
        [FormerlySerializedAs("audioSource1")] [SerializeField] private AudioSource inGameSource;
        [FormerlySerializedAs("audioSource2")] [SerializeField] private AudioSource mainMenuMusicSource;
        [FormerlySerializedAs("audioSource2")] [SerializeField] private AudioSource someMusicSource;

        //[SerializeField] private AudioClip mainMenuMusicClip;
        //[SerializeField] private AudioClip inGameMusicClip;
        
        [SerializeField] private float transitionTime = 1.5f;
        
        [SerializeField] private AudioClip[] inGameMusicList;
        [SerializeField] private AudioClip[] mainMenuMusicList;

        private enum Source { MainMenu, InGame, Some }

        private Source _currentSource = Source.MainMenu;

        public static MusicManager Instance;

        private Coroutine _transitionCoroutine;

        private int _indexMainMusic = 0;
        private int _indexIngameMusic = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ArrayUtils.ShuffleArray(mainMenuMusicList);
                ArrayUtils.ShuffleArray(inGameMusicList);
                foreach (AudioClip clip in inGameMusicList)
                {
                    clip.LoadAudioData();
                }
                foreach (AudioClip clip in mainMenuMusicList)
                {
                    clip.LoadAudioData();
                }
                mainMenuMusicSource.clip = mainMenuMusicList[_indexMainMusic];
                inGameSource.clip = inGameMusicList[_indexMainMusic];
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (_currentSource == Source.InGame)
            {
                if (!inGameSource.isPlaying)
                {
                    Debug.Log("inGame song ended!");
                    _indexIngameMusic++;
                    if(_indexIngameMusic >= inGameMusicList.Length) _indexIngameMusic = 0;
                    inGameSource.clip = inGameMusicList[_indexIngameMusic];
                    inGameSource.Play();
                }
            }
            else if (_currentSource == Source.MainMenu)
            {
                if (!mainMenuMusicSource.isPlaying)
                {
                    Debug.Log("main menu song ended!");
                    _indexMainMusic++;
                    if(_indexMainMusic >= mainMenuMusicList.Length) _indexMainMusic = 0;
                    mainMenuMusicSource.clip = mainMenuMusicList[_indexMainMusic];
                    mainMenuMusicSource.Play();
                }
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

        public void Mute()
        {
            someMusicSource.volume = 0f;
            inGameSource.volume = 0f;
            mainMenuMusicSource.volume = 0f;
        }

        public void Unmute()
        {
            someMusicSource.volume = 1f;
            inGameSource.volume = 1f;
            mainMenuMusicSource.volume = 1f;
        }
    }
}
