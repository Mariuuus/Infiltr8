using System.IO;
using __ProjectMain.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.Utilities.Files
{
    public class GameDataUtils
    {
        /// <summary>
        /// Settings for the json serialization & deserialization
        /// </summary>
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        };
        
        private static readonly string Path;
        
        /// <summary>
        /// will dynamically (not at compile time) load the path to a "const like" variable
        /// </summary>
        static GameDataUtils()
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game/");
            Path = Application.persistentDataPath + "/game/" + "progress.json";
            Debug.Log(Path);
        }
        
        /// <summary>
        /// will create a new game data file or load if exists the previous game data
        /// </summary>
        /// <returns>game data of the user!</returns>
        public static GameData LoadData()
        {
            return File.Exists(Path) ? JsonConvert.DeserializeObject<GameData>(File.ReadAllText(Path), JsonSettings) : new GameData("no_name");
        }

        /// <summary>
        /// Quick Saves (overrides the users game data file)
        /// </summary>
        public static void QuickSave(GameData data)
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSettings);
            File.WriteAllText(Path, jsonString);
        }
    }
}