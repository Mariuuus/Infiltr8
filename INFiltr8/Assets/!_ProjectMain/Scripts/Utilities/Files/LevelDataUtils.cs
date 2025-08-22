using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
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
        
        public static void RemoveLocalLevel(string path)
        {
            File.Delete(path);
        }
        
        public static LevelData LoadFileFromPath(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("No file found");

            string fileContents = File.ReadAllText(path);

            return DeserializeFromString(fileContents);
        }

        public static LevelData DeserializeFromString(string fileContents)
        {
            try
            {
                LevelData levelData = JsonConvert.DeserializeObject<LevelData>(fileContents, JsonSettings);
                //fix connection on LevelData:
            
                foreach (var firewall in LevelEditorUtils.FilterComponents(levelData.components, typeof(FireWallComponent)))
                {
                    foreach (var plate in ((FireWallComponent)(firewall)).activationPlates)
                    {
                        plate.fireWall = (FireWallComponent)firewall;
                    }
                }

                foreach (var activationCom in LevelEditorUtils.FilterComponents(levelData.components,
                             typeof(ActivationComponent)))
                {
                    levelData.components.Remove(activationCom);
                }

                return levelData;
            }
            catch (Exception e)
            {
                Debug.LogError("File is corrupted ("+fileContents+")");
            }

            return null;
        }

        public static LevelData LoadFile(string levelName)
        {
            Debug.Log("Loading game, " + levelName + " from " + Application.persistentDataPath);

            string filename = LevelDataUtils.ReceiveFileName(levelName);

            return LoadFileFromPath(filename);
        }

        public static string SerializeToString(LevelData levelData)
        {
            return JsonConvert.SerializeObject(levelData, JsonSettings);
        }
        
        public static void SaveFile(LevelData levelData, bool overwrite = false)
        {
            Debug.Log("Saving game");
            string filename = LevelDataUtils.ReceiveFileName(levelData.levelName);

            if (!overwrite && File.Exists(filename)) throw new FileLoadException("File already exists");
            
            string jsonString = SerializeToString(levelData);
            
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
        
        public static List<LevelData> GetAvailableLocalLevels()
        {
            string searchPath = Application.persistentDataPath;
            List<LevelData> levels = new List<LevelData>();

            foreach (var level in Directory.GetFiles(searchPath, "*.json", SearchOption.TopDirectoryOnly).ToList())
            {
                levels.Add(LoadFile(level.Replace(".json", "").Replace(Application.persistentDataPath, "").Replace("\\", "").Replace("/","")));
            }
            
            return levels;
        }
        
        public static List<LevelData> SearchTop5(List<LevelData> allLevels, string searchStr)
        {
            if (string.IsNullOrWhiteSpace(searchStr))
                return allLevels.Take(5).ToList();

            var scored = allLevels.Select(level =>
            {
                int distance = StringUtils.LevenshteinDistance(level.levelName, searchStr);
                return new { Level = level, Score = distance };
            });

            var top5 = scored.OrderBy(x => x.Score)
                .ThenBy(x => x.Level.levelName)
                .Take(5)
                .Select(x => x.Level)
                .ToList();

            return top5;
        }
    }
}