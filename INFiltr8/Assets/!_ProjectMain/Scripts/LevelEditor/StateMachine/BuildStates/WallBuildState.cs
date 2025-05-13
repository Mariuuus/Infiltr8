using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class WallBuildState : TwoPointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if(!base.Build(pos1, pos2)) return false;
            PlaceWall(pos1, pos2);
            return true;
        }

        public void PlaceWall(Vector3Int pos1, Vector3Int pos2)
        {
            WallComponent newWall = new WallComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelFileManager.Instance.levelData
            );
            LevelFileManager.Instance.levelData.components.Add(newWall);
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.wallBuildSprite;
        }

        public string GetName()
        {
            return "Wall";
        }
    }
}