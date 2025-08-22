using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class CollectableBuildState : OnePointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos)
        {
            var collectable = new CollectableComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelEditorFileManager.Instance.levelData,
                DistroType.Arch
                );
            LevelEditorFileManager.Instance.levelData.components.Add(collectable);
            return base.Build(pos);
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.collectableSprite;
        }

        public string GetName()
        {
            return "Collectable";
        }
    }
}