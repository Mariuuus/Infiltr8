using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;

namespace __ProjectMain.Scripts.States.Components
{
    [System.Serializable]
    public class WallComponent : TwoPointsLevelComponent
    {
        public WallComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            // check if wall is vertical or horizontal
            if (!(level.wallPointOne.x == level.wallPointTwo.x || level.wallPointOne.y == level.wallPointTwo.y)) throw new InvalidLevelEditorException("Invalid wall points (should be horizontal or vertical)!");
        }
    }
}