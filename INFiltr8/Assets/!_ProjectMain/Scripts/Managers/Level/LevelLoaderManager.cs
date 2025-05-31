using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Objects.PlaceableComponents;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.Level
{
    public class LevelLoaderManager : MonoBehaviour
    {
        public static LevelLoaderManager Instance { get; private set; }

        [SerializeField] private string fallbackLevelName;

        [Header("Tilemap GameObjects")]
        public GameObject floorObject;
        public /*IPlaceable<WallComponent>*/ GameObject wallObject;
        public /*IPlaceable<FireWallComponent>*/ GameObject fireWallObject;
        public /*IPlaceable<ActivationComponent>*/ GameObject activationObject;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Init();
        }

        private void Init()
        {
            LevelData lvlData = LevelFileManager.Instance?.LevelToLoad;
            if (lvlData == null)
            {
                lvlData = LevelDataUtils.LoadFile(fallbackLevelName);
            }
            
            // place ground
            var floor = Instantiate(floorObject);
            floor.GetComponent<FloorPlacer>().PlaceFloor(lvlData);
            
            // place outer walls
            var wallNorth = Instantiate(wallObject);
            var wallEast = Instantiate(wallObject);
            var wallSouth = Instantiate(wallObject);
            var wallWest = Instantiate(wallObject);
            
            wallSouth.GetComponent<WallPlacer>().Place(new WallComponent(
                new Vector2Int(lvlData.wallPointOne.x, lvlData.wallPointOne.y),
                new Vector2Int(lvlData.wallPointTwo.x, lvlData.wallPointOne.y)
            ));
            wallWest.GetComponent<WallPlacer>().Place(new WallComponent(
                new Vector2Int(lvlData.wallPointOne.x, lvlData.wallPointOne.y), 
                new Vector2Int(lvlData.wallPointOne.x, lvlData.wallPointTwo.y) 
            ));
            wallNorth.GetComponent<WallPlacer>().Place(new WallComponent(
                new Vector2Int(lvlData.wallPointOne.x, lvlData.wallPointTwo.y), 
                new Vector2Int(lvlData.wallPointTwo.x, lvlData.wallPointTwo.y)
            ));
            wallEast.GetComponent<WallPlacer>().Place(new WallComponent(
                new Vector2Int(lvlData.wallPointTwo.x, lvlData.wallPointOne.y),
                new Vector2Int(lvlData.wallPointTwo.x, lvlData.wallPointTwo.y)
            ));

            
            // place each component
            foreach (var component in lvlData.components)
            {
                switch (component)
                {
                    case FireWallComponent fireWallComponent:
                    {
                        var newFirewall = Instantiate(fireWallObject);
                        newFirewall.GetComponent<FireWallPlacer>().Place(fireWallComponent);
                        foreach (var activationComponent  in fireWallComponent.activationPlates)
                        {
                            var newActivationPlate = Instantiate(activationObject);
                            newActivationPlate.GetComponent<ActivationPlatePlacer>().Place(activationComponent, newFirewall);
                            break;
                        }
                        break;
                    }
                    case WallComponent wallComponent:
                    {
                        var newObj = Instantiate(wallObject);
                        newObj.GetComponent<WallPlacer>().Place(wallComponent);
                        break;
                    }
                }
            }
            
            //TODO: place player
            
        }
    }
}