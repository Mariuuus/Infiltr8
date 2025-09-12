using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class CameraBuildState: OnePointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos)
        {
            var decoration = new CameraComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelEditorFileManager.Instance.levelData,
                0f,
                25,
                2,
                false
            );
            LevelEditorFileManager.Instance.levelData.components.Add(decoration);
            return base.Build(pos);
        }
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.cameraSprite;
        }

        public string GetName()
        {
            return "Camera";
        }
    }
}