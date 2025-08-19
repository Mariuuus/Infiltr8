using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.UI.MainMenuElements;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.MainMenu
{
    public class LevelLoaderManager : MonoBehaviour
    {
        public static LevelLoaderManager Instance { get; private set; }
        
        public LevelData selectedLevelData;
        public int levelIndex;

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
            SceneManager.LoadScene("LevelLoader");
        }
        
        public void LoadLocalLevel(LevelData levelData)
        {
            selectedLevelData = levelData;
            SceneManager.LoadScene("LevelLoader");
        }
        
        public void LoadLocalLevelEdit(LevelData levelData)
        {
            selectedLevelData = levelData;
            SceneManager.LoadScene("LevelEditor");
        }
    }
}