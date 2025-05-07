using System;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelMaker
{
    public class LevelUIManager : MonoBehaviour
    {
        public TMP_Text lookAtText;
        public Grid grid;
        public Tile hoverTile;
        
        
        private Vector3Int _previousMousePos = new Vector3Int();

        private void Update()
        {
            Vector3Int mousePos = GetMousePosition();
            lookAtText.text = mousePos.ToString();
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