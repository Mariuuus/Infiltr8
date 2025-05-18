using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class DeviceComponent : OnePointLevelComponent
    {
        public List<HackStatus> possibleHacks;
        public HackStatus startHack;

        public DeviceComponent()
        {
            // just for deserialization
        }

        public DeviceComponent(Vector2Int position, LevelData levelData, List<HackStatus> possibleHacks)
        {
            this.position = position;
            this.possibleHacks = new List<HackStatus> { HackStatus.UnHacked };
            this.possibleHacks.AddRange(possibleHacks);
            this.startHack = HackStatus.UnHacked;
        }
        
        public DeviceComponent(Vector2Int position, LevelData levelData)
        {
            this.position = position;
            this.possibleHacks = new List<HackStatus> { HackStatus.UnHacked };
            startHack = HackStatus.UnHacked;
        }
    }
}