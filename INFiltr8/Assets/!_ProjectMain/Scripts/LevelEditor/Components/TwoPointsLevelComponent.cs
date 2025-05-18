using System.Collections.Generic;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class TwoPointsLevelComponent : TwoPointsBaseComponent
    {
        public TwoPointsLevelComponent(Vector2Int startPosition, Vector2Int endPosition, LevelData levelData) : base(startPosition, endPosition, levelData)
        {
            if(LevelEditorUtils.IsPositionBlocked(LevelFileManager.Instance.levelData.components, startPosition, endPosition)) throw new InvalidLevelEditorActionException("There is something in the way. Please reconsider your placement!");
        }

        public TwoPointsLevelComponent()
        {
            // just for deserialization
        }
    }
}