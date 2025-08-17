using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class OnlyPlayerDoorComponent : TwoPointsLevelComponent
    {
        public OnlyPlayerDoorComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            // check if wall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        public OnlyPlayerDoorComponent()
        {
            // just for deserialization
        }
    }
}