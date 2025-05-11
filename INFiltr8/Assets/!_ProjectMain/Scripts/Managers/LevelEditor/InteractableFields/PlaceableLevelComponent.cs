using System;
using System.Reflection;
using __ProjectMain.Scripts.States.Components;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields
{
    [CreateAssetMenu(fileName = "NewPlaceableLevelComponent", menuName = "LevelEditor/PlaceableLevelComponent")]
    public class PlaceableLevelComponent : ScriptableObject    
    {
        public Tile TileRepresentation;
        public GameObject InGameRepresentation;
        public GameObject LevelEditorRepresentation;
        public bool TwoPointObject = false;
        public Sprite BuildMenuIcon;
        
        public string OnBuildFunction;
        
        public void Build()
        {
            if (GetType().GetMethod(OnBuildFunction) != null)
            {
                MethodInfo method = GetType().GetMethod(OnBuildFunction);
                method.Invoke(this, new object[] {});
            }
        }


        public static void PlaceWall()
        {
            Debug.Log("Place wall");
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
                LevelFileManager.Instance.levelData.components.Add(newWall);
                LevelUIManager.Instance.UpdateUI();
                LevelFileManager.Instance.QuickSave();
            }
            catch (InvalidLevelEditorException e)
            {
                //TODO: display Errors!
                Debug.LogError(e.Message);
            }
        }
        
        public void AnotherFunction()
        {
            Debug.Log("AnotherFunction called");
        }
        
        
    }
    
    
}
