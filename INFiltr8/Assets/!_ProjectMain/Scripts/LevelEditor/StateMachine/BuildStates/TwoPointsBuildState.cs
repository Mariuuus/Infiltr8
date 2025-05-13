using System;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public abstract class TwoPointsBuildState : BuildState
    {
        protected Vector3Int LatestCellClicked = new Vector3Int(-1,-1,-1);
        protected Vector3Int PreviousCellClicked = new Vector3Int(-1,-1,-1);

        protected virtual bool Build(Vector3Int pos1, Vector3Int pos2)
        {
            if (LatestCellClicked.Equals(new Vector3Int(-1, -1, -1)) ||
                PreviousCellClicked.Equals(new Vector3Int(-1, -1, -1))) return false;
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
            return true;
        }

        public override void Enter()
        {
            base.Enter();
            ClearPointer();
        }

        public override void Exit()
        {
            base.Exit();
            ClearPointer();
        }

        public override void Update()
        {

            base.Update();
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            if (LatestCellClicked.Equals(new Vector3Int(-1,-1,-1))) return;
            
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelFileManager.Instance.levelData);
            foreach (var pos in LevelEditorUtils.ReduceToInBoundsVectors(LevelEditorUtils.GetPointsInBetween(
                         LevelEditorUtils.ReduceToTwoDimensions(LatestCellClicked),
                         LevelEditorUtils.ReduceToTwoDimensions(mousePos)), LevelFileManager.Instance.levelData))
            {
                LevelManager.Instance.uiTilemap.SetTile(pos, LevelEditorManager.Instance.hoverTile);
            }
            
            PreviousMousePos = mousePos;
        }

        public override void OnClick(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            
            PreviousCellClicked = LatestCellClicked;
            LatestCellClicked = LevelEditorManager.Instance.GetMousePosition();
            try
            {
                if (!Build(LatestCellClicked, PreviousCellClicked)) return;
                base.OnClick(ctx);
                ClearPointer(); 
            }
            catch (InvalidLevelEditorException e)
            {
                Debug.LogException(e);
                Debug.Log(LatestCellClicked);
                Debug.Log(PreviousCellClicked);
                throw e;
            }
        }

        public override void OnEsc(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if(LatestCellClicked.Equals(new Vector3Int(-1, -1, -1)) && PreviousCellClicked.Equals(new Vector3Int(-1, -1, -1))) base.OnEsc(ctx);
                ClearPointer();
            }
        }

        private void ClearPointer()
        {
            LatestCellClicked = new Vector3Int(-1,-1,-1);
            PreviousCellClicked = new Vector3Int(-1,-1,-1);
        }
    }
}