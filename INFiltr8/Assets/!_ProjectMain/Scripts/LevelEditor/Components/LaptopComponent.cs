using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class LaptopComponent : DeviceComponent
    {

        public LaptopComponent()
        {
            // just for deserialization
        }
        
        public LaptopComponent(Vector2Int position, LevelData levelData, List<HackStatus> possibleHacks) : base(position, levelData, possibleHacks)
        {
        }
    }
}