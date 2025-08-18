using __ProjectMain.Scripts.Managers.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class DialogAreaComponent : TwoPointsLevelComponent, IAdjustableComponent
    {
        public DialogData dialog;

        public DialogAreaComponent() {}

        public DialogAreaComponent(Vector2Int startPoint, Vector2Int endPoint, LevelData levelData) : base(startPoint,
            endPoint, levelData)
        {
            this.dialog = ScriptableObject.CreateInstance<DialogData>();
        }

        public void OnAdjust()
        {
            LevelEditorManager.Instance.dialogAreaSettings.Show(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.dialogAreaSettings.Hide();
        }
    }
}
