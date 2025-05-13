using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Utilities.LevelEditor
{
    public class LevelEditorUtils
    {
        public static List<LevelComponent> FilterComponents(List<LevelComponent> components, Type componentType)
        {
            List<LevelComponent> filteredList = new List<LevelComponent>();
            foreach (LevelComponent component in components)
            {
                if(component.GetType() == componentType)
                {
                    filteredList.Add(component);
                }
            }
            return filteredList;
        }

        public static bool IsPointInWall(List<LevelComponent> components, Vector2Int position)
        {
            foreach (LevelComponent component in FilterComponents(components, typeof(WallComponent)))
            {
                if (((WallComponent)component).IsPointInside(position))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPositionBlocked(List<LevelComponent> components, Vector2Int position)
        {
            //Debug.Log(components);
            foreach (var component in components)
            {
                if (component.GetType() == typeof(OnePointLevelComponent))
                {
                    if(((OnePointLevelComponent)component).position.Equals(position)) return true;
                }
                else //two points
                {
                    foreach (var inBetweenPoint in ((TwoPointsLevelComponent) component).GetPointsInBetween())
                    {
                        if(inBetweenPoint.Equals(position)) return true;
                    }
                }
            }
            return false;
        }
        
        public static bool IsPositionBlocked(List<LevelComponent> components, Vector2Int positionOne, Vector2Int positionTwo)
        {
            foreach (var point in GetPointsInBetween(positionOne, positionTwo))
            {
                if(IsPositionBlocked(components, point)) return true;
            }

            return false; 
        }


        public static List<Vector2Int> GetPointsInBetween(Vector2Int startPosition, Vector2Int endPosition)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            int bottomBoundaryX = Mathf.Min(startPosition.x, endPosition.x);
            int upperBoundaryX = Mathf.Max(startPosition.x, endPosition.x);
            int bottomBoundaryY = Mathf.Min(startPosition.y, endPosition.y);
            int upperBoundaryY = Mathf.Max(startPosition.y, endPosition.y);
            for (int x = bottomBoundaryX; x <= upperBoundaryX; x++)
            {
                for (int y = bottomBoundaryY; y <= upperBoundaryY; y++)
                {
                    points.Add(new Vector2Int(x, y));
                }
            }
            return points;
        }

        public static bool IsComponentOnField(LevelData level, LevelComponent component)
        {
            if (component.GetType() == typeof(OnePointLevelComponent))
            {
                var onePosComponent = component as OnePointLevelComponent;
                return IsPositionInField(level, onePosComponent!.position);
            }
            else
            {
                var twoPosComponent = component as TwoPointsLevelComponent;
                return IsPositionInField(level, twoPosComponent!.startPosition) || IsPositionInField(level, twoPosComponent.endPosition);

            }
        }
        
        public static bool AreComponentsOnField(LevelData level, List<LevelComponent> components)
        {
            foreach (var component in components)
            {
                if (component.GetType() == typeof(OnePointLevelComponent))
                {
                    var onePosComponent = component as OnePointLevelComponent;
                    if (!IsPositionInField(level, onePosComponent!.position)) return false;
                }
                else
                {
                    var twoPosComponent = component as TwoPointsLevelComponent;
                    if (!(IsPositionInField(level, twoPosComponent!.startPosition) ||
                          IsPositionInField(level, twoPosComponent.endPosition))) return false;
                }
            }

            return true;
        }

        public static List<Vector3Int> ReduceToInBoundsVectors(List<Vector3Int> positions, LevelData level)
        {
            List<Vector3Int> reducedPositions = new List<Vector3Int>();
            int bottomBoundaryX = Mathf.Min(level.wallPointOne.x, level.wallPointTwo.x);
            int upperBoundaryX = Mathf.Max(level.wallPointOne.x, level.wallPointTwo.x);
            int bottomBoundaryY = Mathf.Min(level.wallPointOne.y, level.wallPointTwo.y);
            int upperBoundaryY = Mathf.Max(level.wallPointOne.y, level.wallPointTwo.y);
            foreach (var pos in positions)
            {
                if (pos.x >= bottomBoundaryX && pos.x <= upperBoundaryX && pos.y >= bottomBoundaryY &&
                    pos.y <= upperBoundaryY)
                {
                    reducedPositions.Add(pos);
                } 
            }
            return reducedPositions;
        }
        public static List<Vector3Int> ReduceToInBoundsVectors(List<Vector2Int> positions, LevelData level)
        {
            return ReduceToInBoundsVectors(ExpandToThreeDimensions(positions), level);
        }

        public static Vector2Int ReduceToTwoDimensions(Vector3Int position)
        {
            return new Vector2Int(position.x, position.y);
        }
        
        public static Vector2Int ReduceToTwoDimensions(Vector3 position)
        {
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        }
        
        public static Vector3Int ExpandToThreeDimensions(Vector2Int position)
        {
            return new Vector3Int(position.x, position.y);
        }
        
        public static List<Vector3Int> ExpandToThreeDimensions(List<Vector2Int> position)
        {
            var result = new List<Vector3Int>();
            foreach (var pos in position)
            {
                result.Add(ExpandToThreeDimensions(pos));
            }
            return result;
        }

        public static void ClearTilemap(Tilemap tilemap, LevelData level)
        {
            foreach (var pos in GetPointsInBetween(level.wallPointOne, level.wallPointTwo))
            {
                tilemap.SetTile(ExpandToThreeDimensions(pos), null);
            }
        }

        private static bool IsPositionInField(LevelData level, Vector2Int position)
        {
            return position.x > level.wallPointOne.x && position.y < level.wallPointOne.y;
        }
        
    }
}