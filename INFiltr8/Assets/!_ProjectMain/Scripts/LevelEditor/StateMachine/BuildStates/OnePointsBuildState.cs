using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public abstract class OnePointsBuildState : BuildState
    { 
        public Vector3Int LatestCellClicked = new Vector3Int(-1,-1,-1);

        public override void OnEsc(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if(LatestCellClicked.Equals(new Vector3Int(-1,-1,-1)))
                {
                    base.OnClick(ctx);
                } else {
                    LatestCellClicked = new Vector3Int(-1,-1,-1);
                }
            }
        }

        public override void Update()
        {
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            if (mousePos != PreviousMousePos)
            {
                LevelManager.Instance.uiTilemap.SetTile(PreviousMousePos, null);
                LevelManager.Instance.uiTilemap.SetTile(mousePos, LevelEditorManager.Instance.hoverTile);
            }
            base.Update();
        }
        
        protected virtual bool Build(Vector3Int pos)
        {
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
            return true;
        }

        public override void OnClick(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                LatestCellClicked = LevelEditorManager.Instance.GetMousePosition();
                try
                {
                    if (!Build(LatestCellClicked)) return;
                    base.OnClick(ctx);
                }
                catch (InvalidLevelEditorActionException e)
                {
                    LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
                    Debug.LogException(e);
                    throw e;
                }
            }
        }
    }
}