using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class ActivationPlateBuildState : TwoPointsBuildState, ISelectableState
    {
        public static FireWallComponent LatestFireWallEdited;
        
        protected override bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            ActivationComponent newActivationComponent = new ActivationComponent(
                LatestFireWallEdited,
                LevelEditorUtils.ReduceToTwoDimensions(pos1),
                LevelEditorUtils.ReduceToTwoDimensions(pos2),
                LevelFileManager.Instance.levelData
            );
            LevelFileManager.Instance.levelData.components.Add(newActivationComponent);
            return true;
        }

        public Sprite GetIcon()
        {
            throw new Exception("this should never happen!");
        }

        public string GetName()
        {
            return "Activation Plate";
        }
    }
}