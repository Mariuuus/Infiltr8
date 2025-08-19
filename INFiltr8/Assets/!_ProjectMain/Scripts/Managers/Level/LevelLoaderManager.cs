using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.Objects.PlaceableComponents;
using __ProjectMain.Scripts.Utilities.Files;
using __ProjectMain.Scripts.Utilities.LevelEditor;
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
        public /*IPlaceable<LaptopComponent>*/ GameObject goalObject;
        public /*IPlaceable<LaptopComponent>*/ GameObject portObject;
        public /*IPlaceable<LaptopComponent>*/ GameObject onlyPlayerWall;
        
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
            LevelData lvlData = MainMenu.LevelLoaderManager.Instance?.selectedLevelData;
            if (lvlData == null)
            {
                lvlData = LevelFileManager.Instance?.LevelToLoad;
                if (lvlData == null)
                {
                    lvlData = LevelDataUtils.LoadFile(fallbackLevelName);
                }
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
            
            var firewalls = LevelEditorUtils.FilterComponents((lvlData.components), typeof(FireWallComponent));
            foreach (var levelComponent in firewalls)
            {
                var fireWallComponent = (FireWallComponent)levelComponent;
                var newFirewall = Instantiate(fireWallObject);
                newFirewall.GetComponent<FireWallPlacer>().Place(fireWallComponent);
                foreach (var activationComponent  in fireWallComponent.activationPlates)
                {
                    var newActivationPlate = Instantiate(activationObject);
                    newActivationPlate.GetComponent<ActivationPlatePlacer>().Place(activationComponent, newFirewall, fireWallComponent);
                }
            }
            
            // place each component
            foreach (var component in lvlData.components)
            {
                switch (component)
                {
                    
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
                    case GoalComponent goalComponent:
                    {
                        var goal = Instantiate(goalObject);
                        goal.transform.position = new Vector3(goalComponent.position.y, -2f, goalComponent.position.x);
                        break;
                    }
                    case OnlyPlayerDoorComponent onlyPlayerWallComponent:
                    {
                        var newObj = Instantiate(onlyPlayerWall);
                        newObj.GetComponent<OnlyPlayerDoorPlacer>().Place(onlyPlayerWallComponent, playerObject.GetComponent<CapsuleCollider>());
                        break;
                    }
                }
            }
            
            
            // Handle specific cause of color :)
            var ports = LevelEditorUtils.FilterComponents((lvlData.components), typeof(PortComponent));
            int totalPorts = ports.Count;
            for (int i = 0; i < totalPorts; i++)
            {
                var portComponent = (PortComponent)ports[i];
                var port1 = Instantiate(portObject);
                var port2 = Instantiate(portObject);

                float hue = (float)i / totalPorts;
                Color pairColor = Color.HSVToRGB(hue, 1f, 1f);
                pairColor *= 6f;

                port1.GetComponent<PortalPlacer>().Place(
                    portComponent, 
                    portComponent.position1, 
                    port2.GetComponent<PortalController>(), 
                    pairColor
                );

                port2.GetComponent<PortalPlacer>().Place(
                    portComponent, 
                    portComponent.position2, 
                    port1.GetComponent<PortalController>(), 
                    pairColor
                );
            }

            if (spawnPoint != null)
            {
                var spawnPointObj = Instantiate(spawnPointObject);
                spawnPointObj.transform.position = new Vector3(spawnPoint.position.y, 0.5f, spawnPoint.position.x);
                
                playerObject.transform.position = spawnPointObj.transform.position;
            }
            else
            {
                throw new ApplicationException("LevelLoaderManager: SpawnPoint not found");
            }
            

        }
    }
}   