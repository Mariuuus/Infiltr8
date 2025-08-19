using System;
using System.Collections;
using System.IO;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using Newtonsoft.Json;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelEditorFileManager : MonoBehaviour
    {
        // Singleton instance
        public static LevelEditorFileManager Instance { get; private set; }

        public string levelName;
        public LevelData levelData;

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        
        public void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            if (MainMenu.LevelLoaderManager.Instance != null)
            {
                levelData = MainMenu.LevelLoaderManager.Instance.selectedLevelData;
            }
            else
            {
                if (!string.IsNullOrEmpty(levelName) && levelData != null)
                {
                    try
                    {
                        levelData = LevelDataUtils.LoadFile(levelName);
                    }
                    catch (FileNotFoundException fileNotFoundException)
                    {
                        Debug.Log(fileNotFoundException.Message);
                        levelData = CreateLevel(levelName);
                        LevelDataUtils.SaveFile(levelData);
                    }
                }
                else
                {
                    int counter = 0;
                    while (true)
                    {
                        counter++;
                        try
                        {
                            LevelData levelData = CreateLevel("Untitled" + counter);
                            LevelDataUtils.SaveFile(levelData);
                            this.levelData = levelData;
                            return;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
        }

        public LevelData CreateLevel(string levelName)
        {
            return new LevelData(levelName);
        }

        

        public void QuickSave()
        {
            Debug.Log("LevelFileManager::QuickSave");
            LevelDataUtils.SaveFile(levelData, overwrite:true);
        }
    }
}
