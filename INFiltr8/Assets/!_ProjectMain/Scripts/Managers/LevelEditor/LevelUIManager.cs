using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields;
using __ProjectMain.Scripts.States;
using __ProjectMain.Scripts.States.Components;
using __ProjectMain.Scripts.Utilities.Exceptions;
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
        public Vector3Int LatestCellClicked { get; private set; }
        public Vector3Int PreviousCellClicked { get; private set; }

        public int currentSelectedBuildComponent = 0;
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public Grid grid;
        public bool hoverActivated = false;
        public PlaceableComponentWithCallback[] BuildComponents;        
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
            if (!hoverActivated) return;
            if (!mousePos.Equals(_previousMousePos)) {
                LevelManager.Instance.uiTilemap.SetTile(_previousMousePos, null);
                LevelManager.Instance.uiTilemap.SetTile(mousePos, hoverTile);
                _previousMousePos = mousePos;
            }
        }

        private void Build()
        {
            InvokeSelectedMethod(BuildComponents[currentSelectedBuildComponent]);
        }   
        
        /*
         * Generated Methode
         */
        private void InvokeSelectedMethod(PlaceableComponentWithCallback item)
        {
            if (item.component == null || string.IsNullOrEmpty(item.selectedMethodName))
            {
                Debug.LogWarning("Component or method name is null");
                return;
            }

            var method = item.component.GetType().GetMethod(item.selectedMethodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null)
            {
                method.Invoke(item.component, null);
            }
            else
            {
                Debug.LogWarning($"Method {item.selectedMethodName} not found on {item.component.name}");
            }
        }
        
        public void OnClick(InputAction.CallbackContext ctx)
        {
            Debug.Log("OnClick in action");
            if (ctx.started)
            {
                if (LevelEditorMode == LevelEditorMode.PlaceOneClick)
                {
                    Debug.Log("OnClick in place one click");
                    LatestCellClicked = GetMousePosition();
                    Build();
                } else if (LevelEditorMode == LevelEditorMode.PlaceTwoClick)
                {
                    Debug.Log("OnClick in place one click");
                    PreviousCellClicked = LatestCellClicked;
                    LatestCellClicked = GetMousePosition();
                    Build();
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