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
        
        private LevelEditorStateMachine _levelEditorStateMachine;
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public TMP_Text currentStateText;
        public Grid grid;
        public GameObject buildButton;        
        public GameObject buildUIContainer;    
        public FireWallSettings fireWallSettings;
        public ActivationPlateSettings activationPlateSettings;
        
        [Header("Tiles")]
        public Tile hoverTile;
        public Tile deleteTile;
        public Tile spawnTile;
        public Tile goalTile;
        public Tile laptopTile;
        public Tile activationPlateTile;

        [Header("Build Menu Sprites")]
        public Sprite wallBuildSprite;
        public Sprite fireWallBuildSprite;
        public Sprite spawnPointSprite;
        public Sprite goalSprite;
        public Sprite laptopSprite;
        public Sprite deleteComponentsSprite;
        public Sprite adjustComponentsSprite;
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
        
        public void SelectEditorState(ISelectableState selectableState) => _levelEditorStateMachine.ChangeState((ILevelEditorState)selectableState);
        public void ChangeToSpectator() => _levelEditorStateMachine.ChangeState(_levelEditorStateMachine.SpectateState);
        public void Init()
        {
            Instance = this;
            
            Debug.Log("LevelUIManager::Init");
            
            _levelEditorStateMachine = new LevelEditorStateMachine();
            _selectableStates = new ISelectableState[]
            {
                _levelEditorStateMachine.WallBuildState,
                _levelEditorStateMachine.DeleteComponentsState,
                _levelEditorStateMachine.SpawnPointBuildState,
                _levelEditorStateMachine.GoalBuildState,
                _levelEditorStateMachine.FireWallBuildState,
                _levelEditorStateMachine.AdjustComponentState,
                _levelEditorStateMachine.LaptopBuildState
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
            LevelManager.Instance.UpdateMap();
            ShowRepresentationInTilemap();
        }

        private void ShowRepresentationInTilemap()
        {
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.levelEditorRepresentationTilemap, LevelEditorFileManager.Instance.levelData);

            foreach (var spawnPoint in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(SpawnPointComponent)).Select(component => ((SpawnPointComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(spawnPoint.position), spawnTile);
            }
            
            foreach (var goalComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(GoalComponent)).Select(component => ((GoalComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(goalComponent.position), goalTile);
            }
            
            foreach (var laptop in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(LaptopComponent)).Select(component => ((LaptopComponent)component)))
            {
                LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(laptop.position), laptopTile);
            }
            
            foreach (var component in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(ActivationComponent)))
            {
                var activation = ((ActivationComponent)component);

                foreach (var pos in activation.GetPointsInBetween())
                {
                    LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(pos), activationPlateTile);
                }
                
            }
        }

        private void Update()
        {
            _levelEditorStateMachine.Update();
        }
        
        public void OnClick(InputAction.CallbackContext ctx)
        {
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