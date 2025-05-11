using System.Collections.Generic;
using System.Linq;
using __ProjectMain.Scripts.States;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields
{
    public abstract class InteractableLevelComponentBase
    {
        public string Name;
        public Vector3Int[] GrabablePoints;

        public InteractableLevelComponentBase(string name, Vector3Int[] grabablePoints)
        {
            this.Name = name;
            this.GrabablePoints = grabablePoints;
        }


        public abstract Tile GetUITileRepresentation();
        public abstract bool updateLevel(LevelData levelData, Vector3Int previousPosition, Vector3Int newPosition);

        protected int getIndexofPoint(Vector3Int point)
        {
            for(int i = 0; i < GrabablePoints.Length; i++) {
                if (point.Equals(GrabablePoints[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        

        public List<InteractableLevelComponentBase> GetAllInteractableCellsAt(List<InteractableLevelComponentBase> allCells, Vector3Int position)
        {
            List<InteractableLevelComponentBase> interactableCells = new List<InteractableLevelComponentBase>();
            foreach (var cell in allCells)
            {
                if (cell.GrabablePoints.Contains(position))
                {
                    interactableCells.Add(cell);
                }
            }
            return interactableCells;
        }
    }
}