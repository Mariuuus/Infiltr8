using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class OnePointLevelComponent : LevelComponent
    {
        public Vector2Int position;

        public OnePointLevelComponent(Vector2Int position, LevelData levelData)
        {
            this.position = position;
            if(!LevelEditorUtils.IsPositionInField(LevelFileManager.Instance.levelData, position)) throw new InvalidLevelEditorException("This is outside of the Level!");
            if(LevelEditorUtils.IsPositionBlocked(LevelFileManager.Instance.levelData.components, position)) throw new InvalidLevelEditorException("There is something in the way. Please reconsider your placement!");
        }
        
        public OnePointLevelComponent()
        {
            // just for deserialization
        }
    }
}