using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class OnlyPlayerDoorBuildState: TwoPointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if(!base.Build(pos1, pos2)) return false;
            PlaceWall(pos1, pos2);
            return true;
        }

        public void PlaceWall(Vector3Int pos1, Vector3Int pos2)
        {
            OnlyPlayerDoorComponent newWall = new OnlyPlayerDoorComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelEditorFileManager.Instance.levelData
            );
            LevelEditorFileManager.Instance.levelData.components.Add(newWall);
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.onlyPlayerWallSprite;
        }

        public string GetName()
        {
            return "Wall";
        }
    }
}