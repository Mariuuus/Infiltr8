using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class GoalComponent : OnePointLevelComponent
    {
        // just for deserialization
        public GoalComponent() {}
        
        public GoalComponent(Vector2Int position, LevelData levelData) : base(position, levelData)
        {
            // Throw Exceptions if needed (One Point/ Two Point Component will check for boundaries etc.)
        }
    }
}