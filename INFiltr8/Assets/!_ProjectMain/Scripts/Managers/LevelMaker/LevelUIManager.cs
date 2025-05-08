using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.Managers.States;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelMaker
{
    public class LevelUIManager : MonoBehaviour
    {
        public static LevelUIManager Instance { get; private set; }
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public Grid grid;
        public bool hoverActivated = false;
        
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
            InitUI();
        }

        private void InitUI()
        {
            foreach (var ilComponent in _interactableLevelComponents)
            {
                foreach (var position in ilComponent.GrabablePoints)
                {
                    Debug.Log(position);
                    LevelManager.Instance.uiTilemap.SetTile(position, ilComponent.GetUITileRepresentation());
                }
            }
            //LevelManager.Instance.uiTilemap.SetTile(new Vector3Int(5,5,0), this.levelCornerTile);

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
        
        Vector3Int GetMousePosition () {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return grid.WorldToCell(mouseWorldPos);
        }
    }
}