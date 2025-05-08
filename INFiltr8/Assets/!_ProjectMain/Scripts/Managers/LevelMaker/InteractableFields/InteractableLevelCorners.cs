using __ProjectMain.Scripts.Managers.States;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelMaker
{
    public class InteractableLevelCorners : InteractableLevelComponentBase
    {
        public InteractableLevelCorners(string name, Vector3Int[] grabablePoints) : base(name, grabablePoints)
        {
            
        }

        public override Tile GetUITileRepresentation()
        {
            return LevelUIManager.Instance.levelCornerTile;
        }

        public override bool updateLevel(LevelData levelData, Vector3Int previousPosition, Vector3Int newPosition)
        {
            int indexPrevPoint = getIndexofPoint(previousPosition);
            if (indexPrevPoint == -1) return false;
            
            Vector3Int newPoint = GrabablePoints[indexPrevPoint];
            Vector3Int otherPoint = GrabablePoints[indexPrevPoint == 0 ? 1 : 0];
            
            if(previousPosition == newPosition) return true;
            
            GrabablePoints[indexPrevPoint] = newPosition;
            
            Vector2Int updatedPoint =  new Vector2Int(newPosition.x, newPosition.y);
            
            levelData.wallPointOne = new Vector2Int(updatedPoint.x, updatedPoint.y);
            levelData.wallPointTwo = new Vector2Int(otherPoint.x, otherPoint.y);
            
            return true;
        }
    }
}