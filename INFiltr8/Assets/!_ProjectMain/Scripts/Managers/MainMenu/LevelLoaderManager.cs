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
        public GameObject levelContainer;
        public GameObject prefabButton;
        public bool cheat;
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

            var level = LevelDataUtils.LoadLevels();
            
            Debug.Log($"Loaded {level.Length} levels");

            for (int i = 0; i < level.Length; i++)
            {
                var levelData = level[i];
                var newObject = Instantiate(prefabButton, levelContainer.transform, false);
                //TODO: add available state in a gamesave folder
                bool available = cheat;
                newObject.GetComponent<LevelStartButton>().Init(levelData, i+1, available);
            }
        }

        public void LoadLevel(LevelData levelData)
        {
            selectedLevelData = levelData;
            Debug.Log(selectedLevelData);
            SceneManager.LoadScene("LevelLoader");
        }
    }
}