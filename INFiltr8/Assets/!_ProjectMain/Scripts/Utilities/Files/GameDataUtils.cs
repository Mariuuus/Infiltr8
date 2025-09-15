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

        private static string GetPath(string postFix = "")
        {
            return Application.persistentDataPath + "/game/" + "progress"+postFix+".json";
        }
        
        /// <summary>
        /// will dynamically (not at compile time) load the path to a "const like" variable
        /// </summary>
        static GameDataUtils()
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game/");
        }
        
        /// <summary>
        /// will create a new game data file or load if exists the previous game data
        /// </summary>
        /// <returns>game data of the user!</returns>
        public static GameData LoadData(string postFix)
        {
            return File.Exists(GetPath(postFix)) ? JsonConvert.DeserializeObject<GameData>(File.ReadAllText(GetPath(postFix)), JsonSettings) : new GameData("no_name");
        }
        
        public static bool Exists(string postFix)
        {
            return File.Exists(GetPath(postFix));
        }
        
        public static void DeleteData(string postFix)
        {
            if (Exists(postFix))
            {
                File.Delete(GetPath(postFix));
            }
        }

        /// <summary>
        /// Quick Saves (overrides the users game data file)
        /// </summary>
        public static void QuickSave(GameData data, string postFix="_temp")
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSettings);
            File.WriteAllText(GetPath(postFix), jsonString);
        }
    }
}