using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class DialogAreaBuildState : TwoPointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if(!base.Build(pos1, pos2)) return false;
            DialogAreaComponent newDialogAreaComponent = new DialogAreaComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelEditorFileManager.Instance.levelData
            );
            LevelEditorFileManager.Instance.levelData.components.Add(newDialogAreaComponent);
            return true;
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.dialogAreaSprite;
        }

        public string GetName()
        {
            return "Dialog Area";
        }
    }
}