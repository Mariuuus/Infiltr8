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

        private IAdjustableComponent _selectedComponent = null;
        
        public override void Update()
        {
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            if (mousePos != LookAtTile)
            {
                LevelComponent currentLookAtComponent =
                    LevelEditorUtils.ReceiveComponentAtPosition(LevelEditorFileManager.Instance.levelData.components, mousePos);
                LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelEditorFileManager.Instance.levelData);
                if (currentLookAtComponent is IAdjustableComponent)
                {
                    

                    if (currentLookAtComponent is IConnectedComponent connectedComponent)
                    {
                        Debug.Log("IConnectedComponent found!");
                        foreach (var component in connectedComponent.GetAllLevelComponents())
                        {
                            Debug.Log(component);
                            foreach (var pos in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.ReceiveComponentPoints(component)))
                            {
                                LevelManager.Instance.uiTilemap.SetTile(pos, LevelEditorManager.Instance.deleteTile);
                            }
                        }
                    }
                    else
                    {
                        foreach (var pos in LevelEditorUtils.ExpandToThreeDimensions(LevelEditorUtils.ReceiveComponentPoints(currentLookAtComponent)))
                        {
                            LevelManager.Instance.uiTilemap.SetTile(pos, LevelEditorManager.Instance.deleteTile);
                        }
                    }
                }
                base.Update();
            }
        }

        public override void OnClick(InputAction.CallbackContext ctx)
        {
            LevelComponent currentLookAtComponent = LevelEditorUtils.ReceiveComponentAtPosition(LevelEditorFileManager.Instance.levelData.components, LookAtTile);
            if (currentLookAtComponent is IAdjustableComponent component)
            {
                component.OnAdjust();
                _selectedComponent = component;
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
            _selectedComponent?.OnExitAdjust();
        }
    }
}