
using __ProjectMain.Scripts.Patterns.State;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Managers.GameStateMachine
{
    public interface IGameState : IState
    {
        public void OnEsc(InputAction.CallbackContext ctx);
    }
}