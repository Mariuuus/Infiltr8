using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class DecorationBuildState: OnePointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos)
        {
            var decoration = new DecorationComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelEditorFileManager.Instance.levelData,
                Decorations.Camera,
                0f
            );
            LevelEditorFileManager.Instance.levelData.components.Add(decoration);
            return base.Build(pos);
        }
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.decorationSprite;
        }

        public string GetName()
        {
            return "Decoration";
        }
    }
}