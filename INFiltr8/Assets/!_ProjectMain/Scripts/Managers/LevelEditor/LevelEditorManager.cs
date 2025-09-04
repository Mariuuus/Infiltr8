using System;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.StateMachine;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.UI;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        public static LevelEditorManager Instance { get; private set; }
        private ISelectableState[] _selectableStates;
        
        public bool isSpecting = true;
        public bool OverUI = false;
        
        private LevelEditorStateMachine _levelEditorStateMachine;
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public TMP_Text currentStateText;
        public Grid grid;
        public GameObject buildButton;        
        public GameObject buildUIContainer;    
        public FireWallSettings fireWallSettings;
        public ActivationPlateSettings activationPlateSettings;
        public LaptopSettings laptopSettings;
        public CollectableSettings collectableSettings;
        public DialogAreaSettings dialogAreaSettings;
        public LaserWallSettings laserWallSettings;
        public ErrorToastController errorToastController;
        public LevelEditorLevelSettings levelSettings;
        public DecorationSettings decorationSettings;
        
        [Header("Tiles")]
        public Tile hoverTile;
        public Tile deleteTile;
        public Tile spawnTile;
        public Tile goalTile;
        public Tile laptopTile;
        public Tile activationPlateTile;
        public Tile portTile;
        public Tile onlyPlayerWallTile;
        public Tile dialogAreaTile;
        public Tile laserWallTile;
        public Tile collectableTile;
        public Tile decorationTile;

        [Header("Build Menu Sprites")]
        public Sprite wallBuildSprite;
        public Sprite fireWallBuildSprite;
        public Sprite spawnPointSprite;
        public Sprite goalSprite;
        public Sprite laptopSprite;
        public Sprite deleteComponentsSprite;
        public Sprite adjustComponentsSprite;
        public Sprite portBuildSprite;
        public Sprite onlyPlayerWallSprite;
        public Sprite dialogAreaSprite;
        public Sprite laserWallSprite;
        public Sprite collectableSprite;
        public Sprite decorationSprite;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _levelEditorStateMachine.ChangeState(_levelEditorStateMachine.SpectateState);
        }

        public void DisplayError(string message)
        {
            errorToastController.ShowError(message);
        }
        
        public void SelectEditorState(ISelectableState selectableState) => _levelEditorStateMachine.ChangeState((ILevelEditorState)selectableState);
        public void ChangeToSpectator() => _levelEditorStateMachine.ChangeState(_levelEditorStateMachine.SpectateState);

        public void OnPointerEnterUI() => OverUI = true;
        public void OnPointerExitUI() => OverUI = false;
        public void Init()
        {
            Instance = this;
            
            Debug.Log("LevelUIManager::Init");
            
            _levelEditorStateMachine = new LevelEditorStateMachine();
            _selectableStates = new ISelectableState[]
            {
                _levelEditorStateMachine.WallBuildState,
                _levelEditorStateMachine.OnlyPlayerDoorBuildState,
                _levelEditorStateMachine.DeleteComponentsState,
                _levelEditorStateMachine.SpawnPointBuildState,
                _levelEditorStateMachine.GoalBuildState,
                _levelEditorStateMachine.FireWallBuildState,
                _levelEditorStateMachine.AdjustComponentState,
                _levelEditorStateMachine.LaptopBuildState,
                _levelEditorStateMachine.PortBuildState,
                _levelEditorStateMachine.DialogAreaBuildState,
                _levelEditorStateMachine.CollectableBuildState,
                _levelEditorStateMachine.LaserWallBuildState,
                _levelEditorStateMachine.DecorationBuildState,
            };
            
            foreach (var state in _selectableStates)
            {
                var newObject = Instantiate(buildButton, buildUIContainer.transform, false);
                newObject.GetComponent<LevelEditorButton>().Init(state);
            }
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.levelEditorRepresentationTilemap, LevelEditorFileManager.Instance.levelData);
            LevelManager.Instance.UpdateMap();
            ShowRepresentationInTilemap();
        }

        private void ShowRepresentationInTilemap()
        {

            foreach (var spawnPoint in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(SpawnPointComponent)).Select(component => ((SpawnPointComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(spawnPoint.position), spawnTile);
            }
            
            foreach (var goalComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(GoalComponent)).Select(component => ((GoalComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(goalComponent.position), goalTile);
            }
            
            foreach (var playerOnlyWallC in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(OnlyPlayerDoorComponent)).Select(component => ((OnlyPlayerDoorComponent)component)))
            {
                foreach (var pos in playerOnlyWallC.GetPointsInBetween())
                {
                    LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(pos), onlyPlayerWallTile);
                }
            }
            
            foreach (var portComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(PortComponent)).Select(component => ((PortComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(portComponent.position1), portTile);
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(portComponent.position2), portTile);
            }
            
            foreach (var laptop in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(LaptopComponent)).Select(component => ((LaptopComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(laptop.position), laptopTile);
            }
            
            foreach (var collectable in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(CollectableComponent)).Select(component => ((CollectableComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(collectable.position), collectableTile);
            }

            foreach (var dialogArea in LevelEditorUtils.FilterComponents(
                LevelEditorFileManager.Instance.levelData.components, typeof(DialogAreaComponent)))
            {
                var dialog = ((DialogAreaComponent) dialogArea);

                foreach (var pos in dialog.GetPointsInBetween())
                {
                    LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(pos), dialogAreaTile);
                }
            }

            foreach (var laserWall in LevelEditorUtils.FilterComponents(
                LevelEditorFileManager.Instance.levelData.components, typeof(LaserWallComponent)))
            {
                var laser = ((LaserWallComponent) laserWall);
                foreach (var pos in laser.GetPointsInBetween())
                {
                    LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(pos), laserWallTile);
                }
            }
            
            foreach (var decoration in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(DecorationComponent)).Select(component => ((DecorationComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(decoration.position), decorationTile);
            }
        }

        private void Update()
        {
            _levelEditorStateMachine.Update();
        }
        
        public void OnClick(InputAction.CallbackContext ctx)
        {
            if (OverUI) return;
            _levelEditorStateMachine.OnClick(ctx);
        }
        
        public void OnEsc(InputAction.CallbackContext ctx)
        {
            _levelEditorStateMachine.OnEsc(ctx);
        }
        
        public Vector3Int GetMousePosition () {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return grid.WorldToCell(mouseWorldPos);
        }
    }
}