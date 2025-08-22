using System;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [Serializable]
    public class CollectableComponent : OnePointLevelComponent, IAdjustableComponent
    {
        public DistroType type;
        
        // just for deserialization
        public CollectableComponent()
        {
           
        }
        
        public CollectableComponent(Vector2Int position, LevelData levelData, DistroType type) : base(position, levelData)
        {
            this.type = type;
        }

        public void OnAdjust()
        {
            LevelEditorManager.Instance.collectableSettings.ShowAndLoad(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.collectableSettings.Hide(this);
        }
    }
}