using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace __ProjectMain.Scripts.Managers.States
{
    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public Vector2Int leftUpperWall;
        public Vector2Int rightBottomWall;
        public Vector2Int spawnPoint;
        
        public List<SubnetLevelData> subnetworkStates;
        
        public Vector2Int flag;

        public LevelData (string levelName) {
            this.levelName = levelName;
            leftUpperWall = new Vector2Int (0, 0);
            rightBottomWall = new Vector2Int (20, 20);
            spawnPoint = new Vector2Int (1, 1);
            subnetworkStates = new List<SubnetLevelData> ();
            flag = new Vector2Int (10,10);
        }
        
    }
}