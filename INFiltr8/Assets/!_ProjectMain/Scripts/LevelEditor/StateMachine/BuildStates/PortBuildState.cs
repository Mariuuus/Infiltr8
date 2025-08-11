using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class PortBuildState : TwoOnePointBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if(!base.Build(pos1, pos2)) return false;
            PlacePort(pos1, pos2);
            return true;
        }

        private void PlacePort(Vector3Int pos1, Vector3Int pos2)
        {
            PortComponent newWall = new PortComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelEditorFileManager.Instance.levelData
            );
            LevelEditorFileManager.Instance.levelData.components.Add(newWall);
        }
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.portBuildSprite;
        }

        public string GetName()
        {
            return "Port";
        }
    }
}