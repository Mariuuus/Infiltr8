using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.EditStates
{
    public abstract class EditState : ILevelEditorState
    {
        protected Vector3Int LookAtTile = new Vector3Int(-1,-1,-1);
        public virtual void Enter()
        {
            LevelEditorManager.Instance.currentStateText.text = "Current Mode:" + GetType().Name;
        }

        public virtual void Exit()
        {
        }
        
        public virtual void Update()
        {
            Vector3Int mousePos = LevelEditorManager.Instance.GetMousePosition();
            LevelEditorManager.Instance.lookAtText.text = "Mouse at: " + mousePos;
            LookAtTile = mousePos;
        }
        

        public virtual void OnClick(InputAction.CallbackContext ctx)
        {
            LevelEditorUtils.ClearTilemap(LevelManager.Instance.uiTilemap, LevelEditorFileManager.Instance.levelData);
            LevelEditorManager.Instance.UpdateUI();
            //LevelEditorFileManager.Instance.QuickSave();
        }

        public virtual void OnEsc(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) LevelEditorManager.Instance.ChangeToSpectator();
        }
    }
}