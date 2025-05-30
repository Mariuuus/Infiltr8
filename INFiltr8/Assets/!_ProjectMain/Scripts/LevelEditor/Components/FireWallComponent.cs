using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class FireWallComponent : TwoPointsLevelComponent, IConnectedComponent, IAdjustableComponent
    {
        public List<ActivationComponent> activationPlates;
        public List<HackStatusAmount> doorUnlockRequirements;

        
        public FireWallComponent()
        {
            // just for deserialization
        }

        public FireWallComponent(List<ActivationComponent> activationPlates, List<HackStatusAmount> doorUnlockRequirements, Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            this.activationPlates = activationPlates;
            this.doorUnlockRequirements = doorUnlockRequirements;
            // check if firewall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        public FireWallComponent( List<HackStatusAmount> doorUnlockRequirements, Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            activationPlates = new List<ActivationComponent>();
            this.doorUnlockRequirements = doorUnlockRequirements;
            // check if firewall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        /*
         * TODO: check if this might be a problem, because this could overwrite the deserialization
         */
        public FireWallComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            activationPlates = new List<ActivationComponent>();
            doorUnlockRequirements = new List<HackStatusAmount>();
            // check if firewall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        /// <summary>
        /// will add the given "requirement", only if this hack color does not exist already.
        /// </summary>
        /// <param name="requirement"></param>
        /// <exception cref="InvalidLevelEditorActionException"></exception>
        public void AddRequirement(HackStatusAmount requirement)
        {
            var status = requirement.hackStatus;
            foreach (var req in doorUnlockRequirements)
            {
                if (req.hackStatus == status) throw new InvalidLevelEditorActionException("This Hackcolor is already a requirement, please delete it first!");
            }
            doorUnlockRequirements.Add(requirement);
        }
        
        /// <summary>
        /// will be used in level editor.
        /// </summary>
        /// <param name="i"></param>
        /// <exception cref="InvalidLevelEditorActionException"></exception>
        public void RemoveRequirement(int i)
        {
            if(i < 0 || i >= doorUnlockRequirements.Count) throw new InvalidLevelEditorActionException("Index of requirement is out of range!");
            doorUnlockRequirements.RemoveAt(i);
        }
        
        /// <summary>
        /// will be used in level editor.
        /// </summary>
        public void ResetRequirements()
        {
            doorUnlockRequirements.Clear();
        }
        
        /// <summary>
        /// add to list of activation plates.
        /// </summary>
        /// <param name="activationPlate"></param>
        public void AddActivationPlate(ActivationComponent activationPlate)
        {
            activationPlates.Add(activationPlate);
        }
        

        /// <summary>
        /// Will be called when this Component will be deleted
        /// </summary>
        public void OnRemove()
        {
            foreach (var plate in activationPlates)
            {
                LevelEditorFileManager.Instance.levelData.components.Remove(plate);
            }
            activationPlates.Clear();
        }
        
        
        /// <returns>This component and all the activation plates.</returns>
        public List<LevelComponent> GetAllLevelComponents()
        {
            var levelComponents = new List<LevelComponent> { this };
            levelComponents.AddRange(activationPlates);
            return levelComponents;
        }

        public void OnAdjust()
        {
            LevelEditorManager.Instance.fireWallSettings.Show(this);
        }
        
        public void OnExitAdjust()
        {
            LevelEditorManager.Instance.fireWallSettings.Hide();
        }
    }
}