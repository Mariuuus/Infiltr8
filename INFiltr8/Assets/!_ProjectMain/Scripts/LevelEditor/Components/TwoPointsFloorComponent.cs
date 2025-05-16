using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class TwoPointsFloorComponent : TwoPointsBaseComponent
    {
        public TwoPointsFloorComponent()
        {
            // just for deserialization
        }
        
        public TwoPointsFloorComponent(Vector2Int startPosition, Vector2Int endPosition, LevelData levelData) : base(startPosition, endPosition, levelData)
        {
            // TODO: should check if the floor is empty at that space.
        }
    }
}