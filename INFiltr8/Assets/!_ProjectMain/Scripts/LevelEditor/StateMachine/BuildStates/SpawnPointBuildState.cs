using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class SpawnPointBuildState : OnePointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos)
        {
            var spawn = new SpawnPointComponent(LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelFileManager.Instance.levelData);
            LevelFileManager.Instance.levelData.components.Add(spawn);
            return base.Build(pos);
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.spawnPointSprite;
        }

        public string GetName()
        {
            return "SpawnPoint";
        }
    }
}