using System;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        private GameData _gameData;
        
        private readonly List<DistroType> _collectInLevel = new List<DistroType>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            _gameData = GameDataUtils.LoadData();
        }

        private void Start()
        {
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
            if (!(_gameData.progress > levelIndex))
            {
                _gameData.progress = levelIndex + 1;
                GameDataUtils.QuickSave(_gameData);
            }

            if (levelIndex != -1)
            {
                PersistCollectables();
            }
        }

        private void PersistCollectables()
        {
            foreach (var distro in _collectInLevel)
            {
                var element = _gameData.collectedDistros.Find(collectDistro => collectDistro.distroType == distro);
                element.collect = true;
            }
            GameDataUtils.QuickSave(_gameData);
            _collectInLevel.Clear();
            
        }

        public void CollectedCollectable(DistroType distro)
        {
            CheckCollectables();
            _collectInLevel.Add(distro);
            GameDataUtils.QuickSave(_gameData);
        }

        public List<CollectDistro> GetCollectables()
        {
            CheckCollectables();
            return _gameData.collectedDistros;
        }
        
        private void CheckCollectables() {
            if (_gameData.collectedDistros == null || _gameData.collectedDistros.Count != Enum.GetValues(typeof(DistroType)).Length)
            {
                foreach (var obj in Enum.GetValues(typeof(DistroType)))
                {
                    var distro = (DistroType)obj;
                    if (_gameData.collectedDistros.Find((collectDistro) => collectDistro.distroType == distro) == null)
                    { 
                        _gameData.collectedDistros.Add(new CollectDistro(distro, false));
                    }
                }
            }
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