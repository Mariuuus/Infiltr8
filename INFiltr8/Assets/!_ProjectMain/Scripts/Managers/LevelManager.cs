using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private LevelFileManager _levelFileManager;
        
        [Header("Tilemaps")]
        public Tilemap groundTilemap; 
        public Tilemap wallsTilemap;
        
        [Header("Tilemap GameObjects")]
        public GameObject wallObject;
        public GameObject groundObject;

        private void Awake()
        {
            _levelFileManager = GetComponent<LevelFileManager>();
            _levelFileManager.Init();
            UpdateMap();
        }

        public void UpdateMap()
        {
            // fill ground
            for (int y = _levelFileManager.levelData.leftUpperWall.y;
                 y < _levelFileManager.levelData.rightBottomWall.y;
                 y++)
            {
                for (int x = _levelFileManager.levelData.leftUpperWall.x;
                     x < _levelFileManager.levelData.rightBottomWall.x;
                     x++)
                {
                    PlaceObjectAtTile(new Vector3Int(x, y, 0), groundTilemap, groundObject);
                }
            }
            
            // create outer walls
            for (int y = _levelFileManager.levelData.leftUpperWall.y;
                 y < _levelFileManager.levelData.rightBottomWall.y;
                 y++)
            {
                for (int x = _levelFileManager.levelData.leftUpperWall.x;
                     x < _levelFileManager.levelData.rightBottomWall.x;
                     x++)
                {
                    if (x == _levelFileManager.levelData.leftUpperWall.x ||
                        x == _levelFileManager.levelData.rightBottomWall.x - 1 ||
                        y == _levelFileManager.levelData.leftUpperWall.y ||
                        y == _levelFileManager.levelData.rightBottomWall.y - 1)
                    {
                        PlaceObjectAtTile(new Vector3Int(x, y, 0), wallsTilemap, wallObject);
                    }
                }
            }
        }
        
        public void PlaceObjectAtTile(Vector3Int cellPosition, Tilemap tilemap, GameObject prefabToPlace)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPosition);
            GameObject instance = Instantiate(prefabToPlace, worldPos, Quaternion.identity);
            instance.transform.SetParent(tilemap.transform);
        }

    }
}