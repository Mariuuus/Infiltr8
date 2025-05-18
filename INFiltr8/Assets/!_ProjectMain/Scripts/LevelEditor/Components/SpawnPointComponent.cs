using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class SpawnPointComponent : OnePointLevelComponent
    {
        public SpawnPointComponent()
        {
            // just for deserialization
        }
        public SpawnPointComponent(Vector2Int position, LevelData levelData) : base(position, levelData)
        {
            if (LevelEditorUtils.FilterComponents(levelData.components, typeof(SpawnPointComponent)).Count != 0) throw new InvalidLevelEditorActionException("There is already a spawn point!");
        }
    }
}