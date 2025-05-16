using System.Collections.Generic;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public enum HackStatus
    {
        UnHacked, RedHacked, GreenHacked, BlueHacked, YellowHacked
    }
    
    [System.Serializable]
    public class Device : OnePointLevelComponent
    {
        private List<HackStatus> possibleHacks;

        public Device()
        {
            // just for deserialization
        }

        public Device(Vector2Int position, LevelData levelData, HackStatus hackStatus, List<HackStatus> possibleHacks)
        {
            this.position = position;
            this.possibleHacks = new List<HackStatus> { HackStatus.UnHacked };
            this.possibleHacks.AddRange(possibleHacks);
        }
    }
}