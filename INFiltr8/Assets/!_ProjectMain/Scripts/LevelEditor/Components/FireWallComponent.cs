using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.StateMachine;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    [System.Serializable]
    public class FireWallComponent : TwoPointsLevelComponent, IConnectedComponent
    {
        public List<ActivationComponent> ActivationPlates { get; }
        public List<HackStatusAmount> DoorUnlockRequirements { get; }

        public FireWallComponent(List<ActivationComponent> activationPlates, List<HackStatusAmount> doorUnlockRequirements, Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            this.ActivationPlates = activationPlates;
            this.DoorUnlockRequirements = doorUnlockRequirements;
            // check if firewall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        public FireWallComponent( List<HackStatusAmount> doorUnlockRequirements, Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            ActivationPlates = new List<ActivationComponent>();
            this.DoorUnlockRequirements = doorUnlockRequirements;
            // check if firewall is vertical or horizontal
            if (!(wallStart.x == wallEnd.x || wallStart.y == wallEnd.y)) throw new InvalidLevelEditorActionException("Invalid wall points (should be horizontal or vertical)!");
        }
        
        /*
         * TODO: check if this might be a problem, because this could overwrite the deserialization
         */
        public FireWallComponent(Vector2Int wallStart, Vector2Int wallEnd, LevelData level) : base(wallStart, wallEnd, level)
        {
            ActivationPlates = new List<ActivationComponent>();
            DoorUnlockRequirements = new List<HackStatusAmount>();
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
            foreach (var req in DoorUnlockRequirements)
            {
                if (req.hackStatus == status) throw new InvalidLevelEditorActionException("This Hackcolor is already a requirement, please delete it first!");
            }
            DoorUnlockRequirements.Add(requirement);
        }
        
        /// <summary>
        /// will be used in level editor.
        /// </summary>
        /// <param name="i"></param>
        /// <exception cref="InvalidLevelEditorActionException"></exception>
        public void RemoveRequirement(int i)
        {
            if(i < 0 || i >= DoorUnlockRequirements.Count) throw new InvalidLevelEditorActionException("Index of requirement is out of range!");
            DoorUnlockRequirements.RemoveAt(i);
        }
        
        /// <summary>
        /// will be used in level editor.
        /// </summary>
        public void ResetRequirements()
        {
            DoorUnlockRequirements.Clear();
        }

        /// <summary>
        /// Will be called when this Component will be deleted
        /// </summary>
        public void OnRemove()
        {
            foreach (var plate in ActivationPlates)
            {
                LevelFileManager.Instance.levelData.components.Remove(plate);
            }
            ActivationPlates.Clear();
        }
        
        
        /// <returns>This component and all the activation plates.</returns>
        public List<LevelComponent> GetAllLevelComponents()
        {
            var levelComponents = new List<LevelComponent> { this };
            levelComponents.AddRange(ActivationPlates);
            return levelComponents;
        }
    }
}