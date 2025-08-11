using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public class PortComponent : TwoOnePointComponent
    {
        public PortComponent(Vector2Int port1, Vector2Int port2,  LevelData levelData)
        {
            this.position1 = port1;
            this.position2 = port2;
        }
    }
}