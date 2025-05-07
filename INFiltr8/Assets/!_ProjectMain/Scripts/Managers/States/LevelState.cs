using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace __ProjectMain.Scripts.Managers.States
{
    [System.Serializable]
    public class LevelState
    {
        public Vector2Int leftUpperWall;
        public Vector2Int rightBottomWall;
        public Transform spawnPoint;
        
        public List<SubnetworkState> subnetworkStates;
        
        public Vector2Int flag;

        public LevelState () {
        }
        
    }
}