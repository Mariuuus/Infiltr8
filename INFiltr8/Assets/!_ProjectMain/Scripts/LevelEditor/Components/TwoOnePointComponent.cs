using System.Collections.Generic;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class TwoOnePointComponent : LevelComponent
    {
        /*
         * startPosition.x <= endPosition.x
         * (cannot do for y, because this would cause 4 options) -> find max value manually (see LevelEditorUtils.GetPointsInBetween)
         */
        public Vector2Int position1;
        public Vector2Int position2;

        public TwoOnePointComponent(Vector2Int position1, Vector2Int position2, LevelData levelData) 
        {
            this.position1 = position1;
            this.position2 = position2;
            
            if(!LevelEditorUtils.IsPositionInField(LevelEditorFileManager.Instance.levelData, position1)) throw new InvalidLevelEditorActionException("This is outside of the Level!");
            if(!LevelEditorUtils.IsPositionInField(LevelEditorFileManager.Instance.levelData, position2)) throw new InvalidLevelEditorActionException("This is outside of the Level!");
        }
        public TwoOnePointComponent()
        {
            // just for deserialization
        }
    }
}