using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.StateMachine;
using Newtonsoft.Json;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class ActivationComponent : TwoPointsFloorComponent, IConnectedComponent, IAdjustableComponent
    {
        public FireWallComponent fireWall;
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
            //TODO: check if this is enough!
        }

        public List<LevelComponent> GetAllLevelComponents()
        {
            return fireWall.GetAllLevelComponents();
        }

        public void OnAdjust()
        {
            fireWall.OnAdjust();
        }

        public void OnExitAdjust()
        {
            fireWall.OnExitAdjust();
        }
    }
}