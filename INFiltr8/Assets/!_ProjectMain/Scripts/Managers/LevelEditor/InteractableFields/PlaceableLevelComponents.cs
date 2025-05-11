using System;
using __ProjectMain.Scripts.States.Components;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields
{
    [CreateAssetMenu(fileName = "PlaceableLevelComponent", menuName = "LevelEditor/PlaceableLevelComponent")]
    public class PlaceableLevelComponents : ScriptableObject
    {
        public Tile TileRepresentation;
        public GameObject InGameRepresentation;
        public GameObject LevelEditorRepresentation;
        public bool TwoPointObject = false;
        public Sprite BuildMenuIcon;

        public void PlaceWall()
        {
            try
            {
                WallComponent newWall = new WallComponent(
                    new Vector2Int(
                        LevelUIManager.Instance.PreviousCellClicked.x,
                        LevelUIManager.Instance.PreviousCellClicked.y
                        ),
                    new Vector2Int(
                        LevelUIManager.Instance.LatestCellClicked.x,
                        LevelUIManager.Instance.LatestCellClicked.y
                        ),
                    LevelFileManager.Instance.levelData
                    );
                LevelFileManager.Instance.levelData.Components.Add(newWall);
                LevelUIManager.Instance.UpdateUI();
                LevelFileManager.Instance.QuickSave();
            }
            catch (InvalidLevelEditorException e)
            {
                //TODO: display Errors!
                Console.WriteLine(e);
            }
        }
    }
}
