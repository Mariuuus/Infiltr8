using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Objects.PlaceableComponents;
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

        private LevelEditorFileManager _levelEditorFileManager;
        private LevelEditorManager _levelEditorManager;
        
        [Header("Tilemaps")]
        public Tilemap uiTilemap;
        public Tilemap levelEditorRepresentationTilemap;
        public Tilemap groundTilemap;
        public Tilemap wallsTilemap;

        [Header("Tilemap GameObjects")]
        public GameObject wallObject;
        public GameObject fireWallObject;
        public GameObject groundObject;
        public GameObject decorationObject;

        private void Awake()
        {
            Active = false;
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
            
            Debug.Log("LevelManager::Init");

            Active = true;

            _levelEditorFileManager = LevelEditorFileManager.Instance;
            _levelEditorFileManager.Init();
            _levelEditorManager = GetComponent<LevelEditorManager>();
            _levelEditorManager.Init();

            UpdateMap();
        }

        public void UpdateMap()
        {
            InitiateGround();

            ClearMap(wallsTilemap);

            InitiateOuterLevelWalls();
            InitiateWalls();
            InitiateFireWalls();
            InitiateDecorations();
        }

        private void InitiateGround()
        {
            ClearMap(groundTilemap);
            for (int y = _levelEditorFileManager.levelData.wallPointOne.y;
                 y <= _levelEditorFileManager.levelData.wallPointTwo.y;
                 y++)
            {
                for (int x = _levelEditorFileManager.levelData.wallPointOne.x;
                     x <= _levelEditorFileManager.levelData.wallPointTwo.x;
                     x++)
                {
                    PlaceObjectAtTile(new Vector3Int(x, y, 0), groundTilemap, groundObject);
                }
            }
        }

        private void InitiateOuterLevelWalls()
        {
            for (int y = _levelEditorFileManager.levelData.wallPointOne.y;
                 y <= _levelEditorFileManager.levelData.wallPointTwo.y;
                 y++)
            {
                for (int x = _levelEditorFileManager.levelData.wallPointOne.x;
                     x <= _levelEditorFileManager.levelData.wallPointTwo.x;
                     x++)
                {
                    if (x == _levelEditorFileManager.levelData.wallPointOne.x ||
                        x == _levelEditorFileManager.levelData.wallPointTwo.x ||
                        y == _levelEditorFileManager.levelData.wallPointOne.y ||
                        y == _levelEditorFileManager.levelData.wallPointTwo.y)
                    {
                        PlaceObjectAtTile(new Vector3Int(x, y, 0), wallsTilemap, wallObject);
                    }
                }
            }
        }

        private void InitiateFireWalls()
        {
            foreach (var levelComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(FireWallComponent)))
            {
                var component = (FireWallComponent)levelComponent;
                foreach (var point in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.GetPointsInBetween(component.startPosition,
                             component.endPosition)))
                {
                    PlaceObjectAtTile(point, wallsTilemap, fireWallObject);
                }
                
                foreach (var activationComponent in component.activationPlates)
                {
                    foreach (var pos in activationComponent.GetPointsInBetween())
                    {
                        levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(pos),  LevelEditorManager.Instance.activationPlateTile);
                    }
                
                }
            }
            
        }

        private void InitiateWalls()
        {
            foreach (var levelComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(WallComponent)))
            {
                var component = (WallComponent)levelComponent;
                foreach (var point in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.GetPointsInBetween(component.startPosition,
                             component.endPosition)))
                {
                    PlaceObjectAtTile(point, wallsTilemap, wallObject);
                }
            }
        }

        private void InitiateDecorations()
        {
            foreach (var levelComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(DecorationComponent)))
            {
                var component = (DecorationComponent)levelComponent;
                var obj = PlaceObjectAtTile(LevelEditorUtils.ExpandToThreeDimensions(component.position), wallsTilemap, decorationObject);
                obj.GetComponent<DecorationPlacer>().Place(component, true);
            }
        }

        public GameObject PlaceObjectAtTile(Vector3Int cellPosition, Tilemap tilemap, GameObject prefabToPlace)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPosition);
            GameObject instance = Instantiate(prefabToPlace, worldPos, Quaternion.identity);
            instance.transform.SetParent(tilemap.transform);
            return instance;
        }
        
        public void ClearMap(Tilemap tilemap)
        {
            foreach (Transform child in tilemap.transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
