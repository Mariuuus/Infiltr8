using System;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        [FormerlySerializedAs("_gameData")] public GameData gameData;
        
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
            gameData = GameDataUtils.LoadData();
        }

        private void Start()
        {
            if (!gameData.introDone)
            {
                SceneManager.LoadScene("!_ProjectMain/Scenes/Intro");
            }

            GameDataUtils.QuickSave(gameData);
        }

        public void WatchedIntro()
        {
            gameData.introDone = true;
            GameDataUtils.QuickSave(gameData);
        }
        
        public void CompletedLevel(int levelIndex)
        {
            if (!(gameData.progress > levelIndex))
            {
                gameData.progress = levelIndex + 1;
                GameDataUtils.QuickSave(gameData);
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
                var element = gameData.collectedDistros.Find(collectDistro => collectDistro.distroType == distro);
                element.collect = true;
            }
            GameDataUtils.QuickSave(gameData);
            _collectInLevel.Clear();
            
        }

        public void CollectedCollectable(DistroType distro)
        {
            CheckCollectables();
            _collectInLevel.Add(distro);
            GameDataUtils.QuickSave(gameData);
        }

        public List<CollectDistro> GetCollectables()
        {
            CheckCollectables();
            return gameData.collectedDistros;
        }
        
        private void CheckCollectables() {
            if (gameData.collectedDistros == null || gameData.collectedDistros.Count != Enum.GetValues(typeof(DistroType)).Length)
            {
                foreach (var obj in Enum.GetValues(typeof(DistroType)))
                {
                    var distro = (DistroType)obj;
                    if (gameData.collectedDistros.Find((collectDistro) => collectDistro.distroType == distro) == null)
                    { 
                        gameData.collectedDistros.Add(new CollectDistro(distro, false));
                    }
                }
            }
        }

        public int ProgressLevel()
        {
            return gameData.progress;
        }

        public void SwitchToOverview()
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }
}