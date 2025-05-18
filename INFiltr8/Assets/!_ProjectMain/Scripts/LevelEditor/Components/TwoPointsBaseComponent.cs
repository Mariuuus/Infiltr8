using System.Collections.Generic;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class TwoPointsBaseComponent : LevelComponent
    {
        /*
         * startPosition.x <= endPosition.x
         * (cannot do for y, because this would cause 4 options) -> find max value manually (see LevelEditorUtils.GetPointsInBetween)
         */
        public Vector2Int startPosition;
        public Vector2Int endPosition;

        public TwoPointsBaseComponent(Vector2Int startPosition, Vector2Int endPosition, LevelData levelData) 
        {
            // check order and switch if messed up!
            if (!(startPosition.x <= endPosition.x))
            {
                this.startPosition = endPosition;
                this.endPosition = startPosition;
            }
            else
            {
                this.startPosition = startPosition;
                this.endPosition = endPosition;
            }
            
            if(!LevelEditorUtils.IsPositionInField(LevelFileManager.Instance.levelData, startPosition)) throw new InvalidLevelEditorActionException("This is outside of the Level!");
            if(!LevelEditorUtils.IsPositionInField(LevelFileManager.Instance.levelData, endPosition)) throw new InvalidLevelEditorActionException("This is outside of the Level!");
        }
        public TwoPointsBaseComponent()
        {
            // just for deserialization
        }

        public bool IsPointInside(Vector2Int position)
        {
            return (startPosition.x < position.x && position.x < endPosition.x ||
                    startPosition.y < position.y && position.y < endPosition.y);
        }

        public List<Vector2Int> GetPointsInBetween()
        {
            return LevelEditorUtils.GetPointsInBetween(startPosition, endPosition);
        }
    }
}