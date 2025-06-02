using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
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

        [Header("GameObjects")]
        public GameObject floorObject;
        public /*IPlaceable<WallComponent>*/ GameObject wallObject;
        public /*IPlaceable<FireWallComponent>*/ GameObject fireWallObject;
        public /*IPlaceable<ActivationComponent>*/ GameObject activationObject;
        public /*IPlaceable<LaptopComponent>*/ GameObject laptopObject;
        public /*IPlaceable<LaptopComponent>*/ GameObject spawnPointObject;
        
        [Header("Player")]
        public GameObject playerObject;


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

            SpawnPointComponent spawnPoint = null;

            
            // place each component
            foreach (var component in lvlData.components)
            {
                switch (component)
                {
                    case FireWallComponent fireWallComponent:
                    {
                        var newFirewall = Instantiate(fireWallObject);
                        newFirewall.GetComponent<FireWallPlacer>().Place(fireWallComponent);
                        Debug.Log(fireWallComponent.activationPlates.Count);
                        foreach (var activationComponent  in fireWallComponent.activationPlates)
                        {
                            var newActivationPlate = Instantiate(activationObject);
                            newActivationPlate.GetComponent<ActivationPlatePlacer>().Place(activationComponent, newFirewall);
                        }
                        break;
                    }
                    case WallComponent wallComponent:
                    {
                        var newObj = Instantiate(wallObject);
                        newObj.GetComponent<WallPlacer>().Place(wallComponent);
                        break;
                    }
                    case LaptopComponent laptopComponent:
                    {
                        var newObj = Instantiate(laptopObject);
                        newObj.GetComponent<LaptopPlacer>().Place(laptopComponent);
                        break;
                    }
                    case SpawnPointComponent spawnPointComponent:
                    {
                        spawnPoint = spawnPointComponent;
                        break;
                    }
                }
            }

            if (spawnPoint != null)
            {
                var spawnPointObj = Instantiate(spawnPointObject);
                spawnPointObj.transform.position = new Vector3(spawnPoint.position.y, 1.5f, spawnPoint.position.x);
                
                playerObject.transform.position = spawnPointObj.transform.position;
            }
            else
            {
                throw new ApplicationException("LevelLoaderManager: SpawnPoint not found");
            }
            

        }
    }
}   