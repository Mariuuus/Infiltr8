using UnityEngine;

namespace __ProjectMain.Scripts.Managers.States
{
    [System.Serializable]
    public class SubnetLevelData
    {
        public Vector2Int leftUpperWall;
        public Vector2Int rightBottomWall;
        public Vector2Int leftUpperDoor;
        public Vector2Int rightBottomDoor;
        // Todo: What is in need to open the door. a list of enums and numbers.
    }
}