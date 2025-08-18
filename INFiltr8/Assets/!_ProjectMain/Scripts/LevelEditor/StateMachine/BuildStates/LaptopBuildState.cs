using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public class LaptopBuildState : OnePointsBuildState, ISelectableState
    {
        protected override bool Build(Vector3Int pos)
        {
            var ph = new List<HackStatus>
            {
                HackStatus.BlueHacked,
                HackStatus.RedHacked,
                HackStatus.GreenHacked,
                HackStatus.YellowHacked
            };
            var spawn = new LaptopComponent(
                LevelEditorUtils.ReduceToTwoDimensions(pos),
                LevelEditorFileManager.Instance.levelData,
                ph);
            LevelEditorFileManager.Instance.levelData.components.Add(spawn);
            return base.Build(pos);
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.laptopSprite;
        }

        public string GetName()
        {
            return "SpawnPoint";
        }
    }
}