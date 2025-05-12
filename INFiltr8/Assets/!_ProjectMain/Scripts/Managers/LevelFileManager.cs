using System;
using System.Collections;
using System.IO;
using __ProjectMain.Scripts.States;
using UnityEngine;
using Newtonsoft.Json;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelFileManager : MonoBehaviour
    {
        // Singleton instance
        public static LevelFileManager Instance { get; private set; }

        public string levelName;
        public LevelData levelData;

        // Json.NET settings for polymorphic serialization
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        private void Awake()
        {
            Debug.Log("LevelFileManager::Awake");

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Init()
        {
            Instance = this;
            Debug.Log("LevelFileManager::Init");
            if (!string.IsNullOrEmpty(levelName) && levelData != null)
            {
                try
                {
                    levelData = LoadFile(levelName);
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    Debug.Log(fileNotFoundException.Message);
                    levelData = CreateLevel(levelName);
                    SaveFile(levelData);
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
                        SaveFile(levelData);
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

        public LevelData CreateLevel(string levelName)
        {
            return new LevelData(levelName);
        }

        private static string ReceiveFileName(string levelName)
        {
            string path = Application.persistentDataPath + "/" + levelName + ".json";
            Debug.Log(path);
            return path;
        }

        public void SaveFile(LevelData levelData, bool overwrite = false)
        {
            Debug.Log("Saving game");
            string filename = ReceiveFileName(levelData.levelName);

            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");

            string jsonString = JsonConvert.SerializeObject(levelData, JsonSettings);
            File.WriteAllText(filename, jsonString);
            Debug.Log("Saved level successfully");
        }

        public LevelData LoadFile(string levelName)
        {
            Debug.Log("Loading game");

            string filename = ReceiveFileName(levelName);

            if (!File.Exists(filename)) throw new FileNotFoundException("No file found");

            string fileContents = File.ReadAllText(filename);

            LevelData levelData = JsonConvert.DeserializeObject<LevelData>(fileContents, JsonSettings);

            Debug.Log("Loaded level successfully");

            return levelData;
        }

        public void QuickSave()
        {
            SaveFile(levelData, true);
        }
    }
}
