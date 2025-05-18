using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class FireWallBuildState : TwoPointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if(!base.Build(pos1, pos2)) return false;
            PlaceFireWall(pos1, pos2);
            return true;
        }

        private void PlaceFireWall(Vector3Int pos1, Vector3Int pos2)
        {
            FireWallComponent newWall = new FireWallComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelFileManager.Instance.levelData
            );
            LevelFileManager.Instance.levelData.components.Add(newWall);
        }
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.fireWallBuildSprite;
        }

        public string GetName()
        {
            return "FireWall";
        }
    }
}