using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.Utilities.Files
{
    public class LevelDataUtils
    {
        public const string LevelFolderName = "./Assets/!_ProjectMain/Data/Levels";
        
        // Json.NET settings for polymorphic serialization
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        
        public static string ReceiveFileName(string levelName)
        {
            string path = Application.persistentDataPath + "/" + levelName + ".json";
            return path;
        }

        public static LevelData[] LoadLevels()
        {
            var directory = Directory.GetFiles(LevelFolderName, "*.json");
            LevelData[] result = new LevelData[directory.Length];
            
            for (int i = 0; i < directory.Length; i++)
            {
                result[i] = (LoadFileFromPath(directory[i]));
            }

            return result;
        }
        
        public static LevelData LoadFileFromPath(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("No file found");

            string fileContents = File.ReadAllText(path);

            LevelData levelData = JsonConvert.DeserializeObject<LevelData>(fileContents, JsonSettings);
            
            return levelData;
        }
        
        public static LevelData LoadFile(string levelName)
        {
            Debug.Log("Loading game, " + levelName + " from " + Application.persistentDataPath);

            string filename = LevelDataUtils.ReceiveFileName(levelName);

            return LoadFileFromPath(filename);
        }
        
        public static void SaveFile(LevelData levelData, bool overwrite = false)
        {
            //Debug.Log("Saving game");
            string filename = LevelDataUtils.ReceiveFileName(levelData.levelName);

            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");

            string jsonString = JsonConvert.SerializeObject(levelData, JsonSettings);
            File.WriteAllText(filename, jsonString);
            //Debug.Log("Saved level successfully");
        }
        
        public static List<LevelData> GetAvailableLevels()
        {
            string searchPath = Application.persistentDataPath;
            List<LevelData> levels = new List<LevelData>();
            foreach (var level in Directory.GetFiles(searchPath, "*.json", SearchOption.AllDirectories).ToList())
            {
                levels.Add(LevelDataUtils.LoadFile(level.Replace(".json", "").Replace(Application.persistentDataPath+"/", "")));
            }

            return levels;
        }
    }
}