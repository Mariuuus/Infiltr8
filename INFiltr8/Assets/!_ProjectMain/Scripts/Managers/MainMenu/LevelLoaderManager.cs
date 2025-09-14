using System;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers.TimeTracker;
using __ProjectMain.Scripts.UI.MainMenuElements;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.MainMenu
{
    public enum LevelType
    {
        None, Regular, LevelEditor, Online
    }
    public class LevelLoaderManager : MonoBehaviour
    {
        public static LevelLoaderManager Instance { get; private set; }
        
        public LevelData selectedLevelData;
        public int levelIndex;
        
        public LevelType currentLevelType =  LevelType.None;

        public bool speedrunMode = false;
        private List<LevelData> _levelDataList = new List<LevelData>();
        private int speedrunLevelIndex = 0;

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadLevel(LevelData levelData, int levelIndex)
        {
            if (GameDataManager.Instance.ProgressLevel() < levelIndex) return;
            selectedLevelData = levelData;
            this.levelIndex = levelIndex;
            currentLevelType = LevelType.Regular;
            SceneManager.LoadScene("LevelLoader");
        }

        public void StartSpeedrun()
        {
            speedrunMode = true;
            _levelDataList = LevelDataUtils.GetAvailableLevels();
            LoadLevel(_levelDataList [speedrunLevelIndex], speedrunLevelIndex);
            SpeedrunTimeTracker.Instance.ResetTime();
            SpeedrunTimeTracker.Instance.ResumeTime();
        }

        public void NextSpeedrunLevel()
        {
            speedrunLevelIndex++;
            if (speedrunLevelIndex > _levelDataList.Count)
            {
                Debug.Log("last level completed (Speedrun finished!)");
                GameDataManager.Instance.gameData.AddSpeedrunEntry(new SpeedrunEntry(SpeedrunTimeTracker.Instance.CurrentTime));
                GameDataUtils.QuickSave(GameDataManager.Instance.gameData);
                GameDataManager.Instance.SwitchToOverview();
                SpeedrunTimeTracker.Instance.PauseTime();
                return;
            }
            LoadLevel(_levelDataList[speedrunLevelIndex], speedrunLevelIndex);
        }
        
        public void LoadLocalLevel(LevelData levelData, bool online=false)
        {
            selectedLevelData = levelData;
            this.levelIndex = -1;
            currentLevelType = online ? LevelType.Online : LevelType.LevelEditor;
            SceneManager.LoadScene("LevelLoader");
        }
        
        public void LoadLocalLevelEdit(LevelData levelData)
        {
            currentLevelType = LevelType.LevelEditor;
            selectedLevelData = levelData;
            SceneManager.LoadScene("LevelEditor");
        }
    }
}