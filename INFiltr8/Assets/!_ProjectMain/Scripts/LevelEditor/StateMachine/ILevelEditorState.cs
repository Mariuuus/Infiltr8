using __ProjectMain.Scripts.Patterns.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public interface ILevelEditorState : IState
    {
        public void OnClick(InputAction.CallbackContext ctx);
        public void OnEsc(InputAction.CallbackContext ctx);
    }
}