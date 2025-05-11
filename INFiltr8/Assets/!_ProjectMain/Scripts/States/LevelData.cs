using System.Collections.Generic;
using __ProjectMain.Scripts.States.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.States
{
    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public Vector2Int wallPointOne;
        public Vector2Int wallPointTwo;
        public Vector2Int spawnPoint;
        
        public List<LevelComponent> components;
        
        public Vector2Int flag;

        public LevelData (string levelName) {
            this.levelName = levelName;
            wallPointOne = new Vector2Int (0, 0);
            wallPointTwo = new Vector2Int (20, 20);
            spawnPoint = new Vector2Int (1, 1);
            components =  new List<LevelComponent>();
            flag = new Vector2Int (10,10);
        }
        
    }
}