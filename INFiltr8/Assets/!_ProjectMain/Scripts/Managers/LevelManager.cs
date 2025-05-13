using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        // Singleton instance
        public static LevelManager Instance { get; private set; }

        public bool Active { get; private set; } = false;

        private LevelFileManager _levelFileManager;
        private LevelEditorManager _levelEditorManager;
        
        [Header("Tilemaps")]
        public Tilemap uiTilemap;
        public Tilemap groundTilemap;
        public Tilemap wallsTilemap;

        [Header("Tilemap GameObjects")]
        public GameObject wallObject;
        public GameObject groundObject;

        private void Awake()
        {
            Active = false;
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Debug.Log("LevelManager::Init");

            Active = true;

            _levelFileManager = GetComponent<LevelFileManager>();
            _levelFileManager.Init();
            _levelEditorManager = GetComponent<LevelEditorManager>();
            _levelEditorManager.Init();

            UpdateMap();
        }

        public void UpdateMap()
        {
            Debug.Log("3");
            for (int y = _levelFileManager.levelData.wallPointOne.y;
                 y <= _levelFileManager.levelData.wallPointTwo.y;
                 y++)
            {
                for (int x = _levelFileManager.levelData.wallPointOne.x;
                     x <= _levelFileManager.levelData.wallPointTwo.x;
                     x++)
                {
                    PlaceObjectAtTile(new Vector3Int(x, y, 0), groundTilemap, groundObject);
                }
            }

            ClearMap(wallsTilemap);

            for (int y = _levelFileManager.levelData.wallPointOne.y;
                 y <= _levelFileManager.levelData.wallPointTwo.y;
                 y++)
            {
                for (int x = _levelFileManager.levelData.wallPointOne.x;
                     x <= _levelFileManager.levelData.wallPointTwo.x;
                     x++)
                {
                    if (x == _levelFileManager.levelData.wallPointOne.x ||
                        x == _levelFileManager.levelData.wallPointTwo.x ||
                        y == _levelFileManager.levelData.wallPointOne.y ||
                        y == _levelFileManager.levelData.wallPointTwo.y)
                    {
                        PlaceObjectAtTile(new Vector3Int(x, y, 0), wallsTilemap, wallObject);
                    }
                }
            }

            foreach (var levelComponent in LevelEditorUtils.FilterComponents(LevelFileManager.Instance.levelData.components, typeof(WallComponent)))
            {
                var component = (WallComponent)levelComponent;
                foreach (var point in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.GetPointsInBetween(component.startPosition,
                             component.endPosition)))
                {
                    PlaceObjectAtTile(point, wallsTilemap, wallObject);

                }
            }

        }
        public void PlaceObjectAtTile(Vector3Int cellPosition, Tilemap tilemap, GameObject prefabToPlace)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPosition);
            GameObject instance = Instantiate(prefabToPlace, worldPos, Quaternion.identity);
            instance.transform.SetParent(tilemap.transform);
        }
        
        public void ClearMap(Tilemap tilemap)
        {
            foreach (Transform child in tilemap.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
