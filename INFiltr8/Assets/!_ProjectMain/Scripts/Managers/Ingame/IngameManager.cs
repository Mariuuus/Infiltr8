using System;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Managers.Level;
using __ProjectMain.Scripts.Managers.TimeTracker;
using __ProjectMain.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.Ingame
{
    public class IngameManager : MonoBehaviour
    {
        public static IngameManager Instance { get; private set; }
        
        public bool Paused { get; private set; }

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            MusicManager.Instance?.PlayInGameMusic();
        }

        public void Pause() {
            Paused = true;
            LevelLoaderManager.Instance.playerObject.GetComponent<PlayerController>().Freeze();
            LevelTimeTracker.Instance.PauseTime();
        }

        public void Resume()
        {
            Paused = false;
            LevelLoaderManager.Instance.playerObject.GetComponent<PlayerController>().UnFreeze();
            LevelTimeTracker.Instance.ResumeTime();
        }

        public void Quit() => GameDataManager.Instance.SwitchToOverview();
    }
}