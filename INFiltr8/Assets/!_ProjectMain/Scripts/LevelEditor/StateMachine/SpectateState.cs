using System;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public class SpectateState : ILevelEditorState
    {
        public void Enter()
        {
            LevelEditorManager.Instance.currentStateText.text = "Current Mode:" + GetType().Name;
            LevelEditorManager.Instance.isSpecting = true;
            LevelEditorManager.Instance.levelSettings.Show(LevelEditorFileManager.Instance.levelData);
        }

        public void Exit()
        {
            LevelEditorManager.Instance.isSpecting = false;
            LevelEditorManager.Instance.levelSettings.Hide();

        }

        public void HandleInput()
        {
        }

        public void Update()
        {
        }

        public void PhysicsUpdate()
        {
        }

        public void OnClick(InputAction.CallbackContext ctx)
        {
        }

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            try
            {
                Debug.Log("Switch to level editor menu state!!!");
                GameDataManager.Instance.SwitchToOverview();
            }
            catch (Exception)
            {
                // is fine: this means not started from main menu
            }
        }
    }
}