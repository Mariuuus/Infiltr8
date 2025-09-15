using System;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.UI.Slots;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;
        public string username = "";
        public string password = "";
        public bool loggedIn = false;
        public int lastLevel;
        public bool playEndSequence = false;
        
        public string postFix = "_temp";

        
        [SerializeField] private GameSlotsManager gameSlotsManager;
        
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
            if(gameSlotsManager == null) gameData = GameDataUtils.LoadData(postFix);
        }
        

        private void Start()
        {
            GetCollectables();
        }

        public void WatchedIntro()
        {
            gameData.introDone = true;
            QuickSave();
        }
        
        public void CompletedLevel(int levelIndex)
        {
            Debug.Log("CompletedLevel called with "+ (levelIndex + 1) + " last level is " + lastLevel);
            if (levelIndex+1 == lastLevel)
            {
                Debug.Log("Completed level");
                PlayEndSequence();
            }
            
            if (!(gameData.progress > levelIndex))
            {
                gameData.progress = levelIndex + 1;
                QuickSave();
            }

            if (levelIndex != -1)
            {
                PersistCollectables();
            }
            
            if (LevelLoaderManager.Instance.speedrunMode)
            {
                LevelLoaderManager.Instance.NextSpeedrunLevel();
            }
        }
        
        public void QuickSave() => GameDataUtils.QuickSave(gameData, postFix);
        

        private void PersistCollectables()
        {
            foreach (var distro in _collectInLevel)
            {
                var element = gameData.collectedDistros.Find(collectDistro => collectDistro.distroType == distro);
                element.collect = true;
            }

            QuickSave();
            _collectInLevel.Clear();
            
        }

        public void CollectedCollectable(DistroType distro)
        {
            CheckCollectables();
            _collectInLevel.Add(distro);
            QuickSave();
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
        
        public void PlayEndSequence()
        {
            playEndSequence = true;
        }
    }
}