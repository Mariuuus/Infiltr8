using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor
{
    [System.Serializable]
    public class LevelData
    {
        [JsonProperty(Order = 1)] public string levelName;
        [JsonProperty(Order = 2)] public Vector2Int wallPointOne;
        [JsonProperty(Order = 3)] public Vector2Int wallPointTwo;
        [JsonProperty(Order = 4)] public List<LevelComponent> components;
        

        public LevelData (string levelName) {
            this.levelName = levelName;
            wallPointOne = new Vector2Int (0, 0);
            wallPointTwo = new Vector2Int (20, 20);
            components =  new List<LevelComponent>();
        }
    }
}