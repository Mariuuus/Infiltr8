using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class WallComponent : TwoPointsLevelComponent
    {
        public WallComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            // check if wall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        public WallComponent()
        {
            // just for deserialization
        }
    }
}