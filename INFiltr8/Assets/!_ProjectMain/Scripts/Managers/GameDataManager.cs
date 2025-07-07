using System;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        private GameData _gameData;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _gameData = GameDataUtils.LoadData();

            if (!_gameData.introDone)
            {
                Debug.Log("introDone");
                CameraManager.Instance.GameStartSequence();
            }

            GameDataUtils.QuickSave(_gameData);
        }

        public void WatchedIntro()
        {
            _gameData.introDone = true;
            GameDataUtils.QuickSave(_gameData);
        }
        
        public void CompletedLevel(int levelIndex)
        {
            if (_gameData.progress > levelIndex) return;
            _gameData.progress = levelIndex+1;
            GameDataUtils.QuickSave(_gameData);
        }

        public int ProgressLevel()
        {
            return _gameData.progress;
        }

        public void SwitchToOverview()
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }
}