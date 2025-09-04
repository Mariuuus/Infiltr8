using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor
{
    public enum LevelType
    {
        GetData, TimeBased, Silent, SilentAndTimeBased
    }
    [System.Serializable]
    public class LevelData
    {
        [JsonProperty(Order = 1)] public string levelName;
        [JsonProperty(Order = 2)] public Vector2Int wallPointOne;
        [JsonProperty(Order = 3)] public Vector2Int wallPointTwo;
        [JsonProperty(Order = 4)] public List<LevelComponent> components;
        [JsonProperty(Order = 5)] public LevelType levelType =  LevelType.GetData;
        [JsonProperty(Order = 6)] public float availableTime = 0f;
        [JsonProperty(Order = 7)] public bool isPartOfTutorial = false;

        public LevelData () {
            // just for deserialization
        }
        public LevelData (string levelName) {
            this.levelName = levelName;
            wallPointOne = new Vector2Int (0, 0);
            wallPointTwo = new Vector2Int (20, 20);
            components =  new List<LevelComponent>();
        }
        
        public LevelData (string levelName, int size) {
            this.levelName = levelName;
            wallPointOne = new Vector2Int (0, 0);
            wallPointTwo = new Vector2Int (size, size);
            components =  new List<LevelComponent>();
        }
        
        public LevelData (string levelName, int height, int width) {
            this.levelName = levelName;
            wallPointOne = new Vector2Int (0, 0);
            wallPointTwo = new Vector2Int (height, width);
            components =  new List<LevelComponent>();
        }
    }
}