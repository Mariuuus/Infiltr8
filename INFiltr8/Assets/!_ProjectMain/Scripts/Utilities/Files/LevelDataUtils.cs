using System.Collections.Generic;
using System.IO;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.Utilities.Files
{
    public class LevelDataUtils
    {
        // Json.NET settings for polymorphic serialization
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
        
        public static string ReceiveFileName(string levelName)
        {
            string path = Application.persistentDataPath + "/" + levelName + ".json";
            return path;
        }
        
        public static LevelData LoadFile(string levelName)
        {
            Debug.Log("Loading game, " + levelName);

            string filename = LevelDataUtils.ReceiveFileName(levelName);

            if (!File.Exists(filename)) throw new FileNotFoundException("No file found");

            string fileContents = File.ReadAllText(filename);

            LevelData levelData = JsonConvert.DeserializeObject<LevelData>(fileContents, JsonSettings);

            Debug.Log("Loaded level successfully");

            return levelData;
        }
        
        public static void SaveFile(LevelData levelData, bool overwrite = false)
        {
            Debug.Log("Saving game");
            string filename = LevelDataUtils.ReceiveFileName(levelData.levelName);

            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");

            string jsonString = JsonConvert.SerializeObject(levelData, JsonSettings);
            File.WriteAllText(filename, jsonString);
            Debug.Log("Saved level successfully");
        }
        
        public static List<LevelData> GetAvailableLevels()
        {
            string searchPath = Application.persistentDataPath;
            List<LevelData> levels = new List<LevelData>();
            foreach (var level in Directory.GetFiles(searchPath, "*.json", SearchOption.AllDirectories).ToList())
            {
                Debug.Log(level);
                levels.Add(LevelDataUtils.LoadFile(level.Replace(".json", "").Replace(Application.persistentDataPath+"/", "")));
            }

            return levels;
        }
    }
}