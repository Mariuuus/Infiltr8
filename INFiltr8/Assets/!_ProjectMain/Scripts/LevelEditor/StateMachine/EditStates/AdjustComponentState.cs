using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.EditStates
{
    public class AdjustComponentState : EditState, ISelectableState
    {
        
        public override void Update()
        {
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            if (mousePos != LookAtTile)
            {
                LevelComponent currentLookAtComponent =
                    LevelEditorUtils.ReceiveComponentAtPosition(LevelFileManager.Instance.levelData.components, mousePos);
                LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
                if (currentLookAtComponent is IAdjustableComponent)
                {
                    foreach (var pos in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.ReceiveComponentPoints(currentLookAtComponent)))
                    {
                        LevelManager.Instance.uiTilemap.SetTile(pos, LevelEditorManager.Instance.deleteTile);
                    }
                }
                base.Update();
            }
        }

        public override void OnClick(InputAction.CallbackContext ctx)
        {
            LevelComponent currentLookAtComponent = LevelEditorUtils.ReceiveComponentAtPosition(LevelFileManager.Instance.levelData.components, LookAtTile);
            if (currentLookAtComponent is IAdjustableComponent component)
            {
                component.OnAdjust();
                base.OnClick(ctx);
            }
        }
        public Sprite GetIcon()
        {
            return LevelEditorManager.Instance.adjustComponentsSprite;
        }

        public string GetName()
        {
            return "Adjust Components";
        }

        public override void Exit()
        {
            //TODO: check how some could handle the close of this window
            base.Exit();
        }
    }
}