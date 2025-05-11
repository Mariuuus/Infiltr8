using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.States.Components
{
    [System.Serializable]
    public class OnePointLevelComponent : LevelComponent
    {
        public Vector2Int position;

        public OnePointLevelComponent(Vector2Int startPosition, LevelData levelData)
        {
            this.position = startPosition;
            if(LevelEditorUtils.IsPositionBlocked(LevelFileManager.Instance.levelData.Components, startPosition)) throw new InvalidLevelEditorException("There is something in the way. Please reconsider your placement!");
        }
    }
}