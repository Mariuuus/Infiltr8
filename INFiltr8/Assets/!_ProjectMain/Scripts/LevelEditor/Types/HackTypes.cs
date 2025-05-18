using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.LevelEditor.Types
{
    [System.Serializable]
    public enum HackStatus
    {
        RedHacked, GreenHacked, BlueHacked, YellowHacked
    }
    
    [System.Serializable]
    public struct HackStatusAmount {
        public HackStatusAmount(HackStatus status, int amount)
        {
            hackStatus = status;
            this.amount = amount;
        }
        public HackStatus hackStatus;
        public int amount;
    }
}