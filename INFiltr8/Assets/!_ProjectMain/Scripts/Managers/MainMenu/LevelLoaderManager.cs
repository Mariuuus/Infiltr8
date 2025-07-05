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

        public void LoadLevel(LevelData levelData)
        {
            selectedLevelData = levelData;
            Debug.Log(selectedLevelData);
            SceneManager.LoadScene("LevelLoader");
        }
    }
}