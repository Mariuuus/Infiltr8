using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.StateMachine;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class ActivationComponent : TwoPointsFloorComponent, IConnectedComponent, IAdjustableComponent
    {
        [SerializeReference] public FireWallComponent fireWall;
        public int maxDevices;
        public ActivationComponent(FireWallComponent fireWall, Vector2Int startPoint, Vector2Int endPoint, LevelData levelData) : base(startPoint,
            endPoint, levelData)
        {
            this.fireWall = fireWall;
            fireWall.AddActivationPlate(this);
        }
        
        public ActivationComponent()
        {
            // just for deserialization
        }
        
        /// <summary>
        /// no need to delete other because this is a child component!, but remove from firewall
        /// </summary>
        public void OnRemove()
        {
            fireWall.activationPlates.Remove(this);
        }

        public List<LevelComponent> GetAllLevelComponents()
        {
            return fireWall.GetAllLevelComponents();
        }

        public void OnAdjust()
        {
            //fireWall.OnAdjust();
            LevelEditorManager.Instance.activationPlateSettings.Show(this);
        }

        public void OnExitAdjust()
        {
            //fireWall.OnExitAdjust();
            LevelEditorManager.Instance.activationPlateSettings.Hide();
        }
    }
}