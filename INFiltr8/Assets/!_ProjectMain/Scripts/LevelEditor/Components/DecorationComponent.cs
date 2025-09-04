using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public class DecorationComponent: OnePointLevelComponent, IAdjustableComponent
    {
        public Decorations decoration;

        public DecorationComponent()
        {
            
        }
        public DecorationComponent(Vector2Int position, LevelData levelData, Decorations type) : base(position,
            levelData)
        {
            this.decoration = type;
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