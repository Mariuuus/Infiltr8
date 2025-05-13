using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.EditStates
{
    public class DeleteComponentsState : EditState, ISelectableState
    {
        public override void Update()
        {
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            if (mousePos != LookAtTile)
            {
                LevelComponent currentLookAtComponent =
                    LevelEditorUtils.ReceiveComponentAtPosition(LevelFileManager.Instance.levelData.components, mousePos);
                LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
                if (currentLookAtComponent != null)
                {
                    foreach (var pos in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.ReceiveComponentPoints(currentLookAtComponent)))
                    {
                        LevelManager.Instance.uiTilemap.SetTile(pos, LevelEditorManager.Instance.deleteTile);
                    }
                }
                base.Update(); // could lead to problem, might need to move one layer down
            }
        }

        public override void OnClick(InputAction.CallbackContext ctx)
        {
            LevelComponent currentLookAtComponent = LevelEditorUtils.ReceiveComponentAtPosition(LevelFileManager.Instance.levelData.components, LookAtTile);
            if (currentLookAtComponent != null)
            {
                LevelFileManager.Instance.levelData.components.Remove(currentLookAtComponent);
                Debug.Log("1");
                base.OnClick(ctx);
            }
        }

        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.deleteComponentsSprite;
        }

        public string GetName()
        {
            return "Delete Components";
        }
    }
}