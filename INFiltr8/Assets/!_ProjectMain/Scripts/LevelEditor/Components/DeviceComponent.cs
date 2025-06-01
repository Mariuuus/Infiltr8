using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class DeviceComponent : OnePointLevelComponent
    {
        public List<HackStatus> possibleHacks;

        public DeviceComponent()
        {
            // just for deserialization
        }

        public DeviceComponent(Vector2Int position, LevelData levelData, List<HackStatus> possibleHacks)
        {
            this.position = position;
            this.possibleHacks = new List<HackStatus> {};
            this.possibleHacks.AddRange(possibleHacks);
        }
        
        public DeviceComponent(Vector2Int position, LevelData levelData)
        {
            this.position = position;
            this.possibleHacks = new List<HackStatus> {};
        }
    }
}