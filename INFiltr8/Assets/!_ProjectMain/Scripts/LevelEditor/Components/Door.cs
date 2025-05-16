using System.Collections.Generic;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public struct HackStatusAmount {
        HackStatus hackStatus;
        int amount;
    }
    
    
    [System.Serializable]
    public class Door : TwoPointsLevelComponent
    {
        private List<ActivationPlate> activationPlates;
        private List<HackStatusAmount> doorUnlockRequirements;

        public Door(List<ActivationPlate> activationPlates, List<HackStatusAmount> doorUnlockRequirements)
        {
            this.activationPlates = activationPlates;
            this.doorUnlockRequirements = doorUnlockRequirements;
        }
    }
}