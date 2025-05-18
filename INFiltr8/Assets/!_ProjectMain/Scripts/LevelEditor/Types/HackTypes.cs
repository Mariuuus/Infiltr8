using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.LevelEditor.Types
{
    [System.Serializable]
    public enum HackStatus
    {
        UnHacked, RedHacked, GreenHacked, BlueHacked, YellowHacked
    }
    
    [System.Serializable]
    public struct HackStatusAmount {
        public HackStatus hackStatus;
        public int amount;
    }
}