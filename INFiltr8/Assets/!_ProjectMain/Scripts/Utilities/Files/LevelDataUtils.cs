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
        private const string LevelFolderName = "Levels";
        
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
        
        public static LevelData LoadFileFromPath(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("No file found");

            string fileContents = File.ReadAllText(path);

            return DeserializeFromString(fileContents);
        }

        private static LevelData DeserializeFromString(string fileContents)
        {
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
            Debug.Log("Saving game");
            string filename = LevelDataUtils.ReceiveFileName(levelData.levelName);

            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");

            string jsonString = JsonConvert.SerializeObject(levelData, JsonSettings);
            File.WriteAllText(filename, jsonString);
            Debug.Log("Saved level successfully");
        }
        
        public static List<LevelData> GetAvailableLevels()
        {
            var assets =  Resources.LoadAll(LevelFolderName, typeof(TextAsset));
            List<LevelData> levels = new List<LevelData>();
            foreach (var file in assets)
            {
                levels.Add(DeserializeFromString(((TextAsset)file).text));
            }
            return levels;
        }
    }
}