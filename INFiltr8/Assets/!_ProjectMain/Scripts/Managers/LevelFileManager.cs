using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using __ProjectMain.Scripts.Managers.States;

namespace __ProjectMain.Scripts.Managers
{  
    public class LevelFileManager : MonoBehaviour
    {
        public string levelName;
        public LevelData levelData;
        
        public void Init()
        {
            if (levelName != "" && levelData != null)
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
                        LevelData levelData = CreateLevel("Untitled"+counter);
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
            string path = Application.persistentDataPath + "/" + levelName+".json";
            Debug.Log(path);
            return path;
        }
        
        public void SaveFile(LevelData levelData, bool overwrite = false)
        {
            Debug.Log("Saving game");
            string filename = ReceiveFileName(levelData.levelName);
            
            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");
            
            string jsonString = JsonUtility.ToJson(levelData, true);
            File.WriteAllText(filename, jsonString);
            Debug.Log("Saved level successfully");
        }

        public LevelData LoadFile(string levelName)
        {
            Debug.Log("Loading game");

            string filename = ReceiveFileName(levelName);
            
            if (!File.Exists(filename)) throw new FileNotFoundException("No file found");
            
            string fileContents = File.ReadAllText(filename);
            LevelData levelData = JsonUtility.FromJson<LevelData>(fileContents);
            
            Debug.Log("Loaded level successfully");
            
            return levelData;
        }
    }
}