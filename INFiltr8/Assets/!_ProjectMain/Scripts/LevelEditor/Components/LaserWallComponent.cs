using System;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;


namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [Serializable]
    public class LaserWallComponent : TwoPointsLevelComponent, IAdjustableComponent
    {
        public float speed = 0f;

        public LaserWallComponent()
        {
            // just for deserialization
        }

        public LaserWallComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd,
            level)
        {
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid laser-wall points (should be horizontal or vertical)!");
        }

        public void OnAdjust()
        {
            LevelEditorManager.Instance.laserWallSettings.Show(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.laserWallSettings.Hide();
        }
    }
}