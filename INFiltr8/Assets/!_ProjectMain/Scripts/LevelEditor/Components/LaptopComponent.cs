using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class LaptopComponent : DeviceComponent, IAdjustableComponent
    {

        public LaptopComponent()
        {
            // just for deserialization
        }
        
        public LaptopComponent(Vector2Int position, LevelData levelData, List<HackStatus> possibleHacks) : base(position, levelData, possibleHacks)
        {
        }

        public void OnAdjust()
        {
            LevelEditorManager.Instance.laptopSettings.ShowAndLoad(this);
        }

        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.laptopSettings.Hide();
        }
    }
}