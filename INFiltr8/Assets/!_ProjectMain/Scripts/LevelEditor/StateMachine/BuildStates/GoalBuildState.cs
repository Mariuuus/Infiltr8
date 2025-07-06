using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class GoalBuildState : OnePointsBuildState, ISelectableState
    {
        
        protected override bool Build(Vector3Int pos)
        {
            var goal = new GoalComponent(LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelEditorFileManager.Instance.levelData);
            LevelEditorFileManager.Instance.levelData.components.Add(goal);
            return base.Build(pos);
        }
        
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.goalSprite;
        }

        public string GetName()
        {
            return "Goal";
        }
    }
}