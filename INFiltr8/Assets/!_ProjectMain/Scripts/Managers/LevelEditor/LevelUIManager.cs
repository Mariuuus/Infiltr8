using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields;
using __ProjectMain.Scripts.States;
using __ProjectMain.Scripts.States.Components;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class LevelUIManager : MonoBehaviour
    {
        public static LevelUIManager Instance { get; private set; }
        public LevelEditorMode LevelEditorMode { get; private set; }
        public Vector3Int LatestCellClicked;
        public Vector3Int PreviousCellClicked;

        public int currentSelectedBuildComponent = 0;
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public Grid grid;
        public bool hoverActivated = false;
        public PlaceableLevelComponent[] BuildComponents;        
        public GameObject BuildButton;        
        public GameObject BuildUIContainer;        
        public GameObject BuildModeOptions;        
        
        [Header("Tiles")]
        public Tile hoverTile;
        public Tile levelCornerTile;
        public Tile spawnPointTile;

        private Vector3Int _previousMousePos = new Vector3Int(-1,-1,-1);
        private List<InteractableLevelComponentBase> _interactableLevelComponents;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LevelEditorMode = LevelEditorMode.Spectate;

            for(int i = 0; i < BuildComponents.Length; i++)
            {
                var newObject = Instantiate(BuildButton, BuildUIContainer.transform, false);
                newObject.GetComponent<BuildButton>().Init(i, BuildComponents[i]);
            }
        }

        public void Init()
        {
            Instance = this;
            Debug.Log("LevelUIManager::Init");
            _interactableLevelComponents = new List<InteractableLevelComponentBase>();
            _interactableLevelComponents.Add(
                new InteractableLevelCorners("LevelCorners", new []
                {
                    new Vector3Int(LevelFileManager.Instance.levelData.wallPointOne.x,LevelFileManager.Instance.levelData.wallPointOne.y,0),
                    new Vector3Int(LevelFileManager.Instance.levelData.wallPointTwo.x,LevelFileManager.Instance.levelData.wallPointTwo.y,0)
                }));
            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (var ilComponent in _interactableLevelComponents)
            {
                foreach (var position in ilComponent.GrabablePoints)
                {
                    Debug.Log(position);
                    LevelManager.Instance.uiTilemap.SetTile(position, ilComponent.GetUITileRepresentation());
                }
            }
        }

        private void Update()
        {
            Vector3Int mousePos = GetMousePosition();
            lookAtText.text = mousePos.ToString();

            switch (LevelEditorMode)
            {
                case LevelEditorMode.Spectate:
                    break;
                case LevelEditorMode.PlaceOneClick:
                    if (!mousePos.Equals(_previousMousePos)) {
                        LevelManager.Instance.uiTilemap.SetTile(_previousMousePos, null);
                        LevelManager.Instance.uiTilemap.SetTile(mousePos, hoverTile);
                        _previousMousePos = mousePos;
                    }
                    break;
                case LevelEditorMode.PlaceTwoClick:
                    LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
                    foreach (var pos in LevelEditorUtils.ReduceToInBoundsVectors(LevelEditorUtils.GetPointsInBetween(
                                 LevelEditorUtils.ReduceToTwoDimensions(LatestCellClicked),
                                 LevelEditorUtils.ReduceToTwoDimensions(mousePos)), LevelFileManager.Instance.levelData))
                    {
                        LevelManager.Instance.uiTilemap.SetTile(pos, hoverTile);
                    }
                    _previousMousePos = mousePos;
                    break;
            }
            if (!hoverActivated) return;

        }

        private void Build()
        {
            BuildComponents[currentSelectedBuildComponent].Build();
        }   
        
        public void OnClick(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (LevelEditorMode == LevelEditorMode.PlaceOneClick)
                {
                    LatestCellClicked = GetMousePosition();
                    Build();
                } else if (LevelEditorMode == LevelEditorMode.PlaceTwoClick)
                {
                    PreviousCellClicked = LatestCellClicked;
                    LatestCellClicked = GetMousePosition();
                    Build();
                    LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);

                }
            }
        }

        public void ChangeOnBuildModeSelection(Int32 modeIndex)
        {
            this.LevelEditorMode = (LevelEditorMode)modeIndex;
        }

        public void ChangeMode(LevelEditorMode mode)
        {
            if (this.LevelEditorMode == mode) return;
            this.LevelEditorMode = mode;
            // TODO: might need some more work here (change other vars etc.)
            // needs some logic
            // PreviousCellClicked = null
            // LatestCellClicked = null;
        }
        
        Vector3Int GetMousePosition () {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return grid.WorldToCell(mouseWorldPos);
        }
    }
}