using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public class DecorationComponent: OnePointLevelComponent, IAdjustableComponent
    {
        public Decorations decoration;
        public float rotation;
        public Variations variant;

        public DecorationComponent()
        {
            
        }
        public DecorationComponent(Vector2Int position, LevelData levelData, Decorations type, float rotation, Variations variant) : base(position,
            levelData)
        {
            this.decoration = type;
            this.rotation = rotation;
            this.variant = variant;
        }

        public void OnAdjust()
        {
           LevelEditorManager.Instance.decorationSettings.Show(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.decorationSettings.Hide();
        }
    }
}