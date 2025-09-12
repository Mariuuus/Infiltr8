using __ProjectMain.Scripts.Managers.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public class CameraComponent:  OnePointLevelComponent, IAdjustableComponent
    {
        public float rotation;
        public float rotationSpeed;
        public int turnaroundTime;
        public bool disableRotation;
        
        // ADD range for camera fov
        
        public CameraComponent()
        {
            
        }

        public CameraComponent(Vector2Int position, LevelData levelData, float rotation, int rotationSpeed, int turnaroundTime, bool disableRotation) : base(position,
            levelData)
        {
            this.rotation = rotation;
            this.rotationSpeed = rotationSpeed;
            this.turnaroundTime = turnaroundTime;
            this.disableRotation = disableRotation;
        }
        public void OnAdjust()
        {
            LevelEditorManager.Instance.cameraSettings.Show(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.cameraSettings.Hide();
        }
    }
}