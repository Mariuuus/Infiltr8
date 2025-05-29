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
        }

        public void Exit()
        {
            LevelEditorManager.Instance.isSpecting = false;
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
        }
    }
}