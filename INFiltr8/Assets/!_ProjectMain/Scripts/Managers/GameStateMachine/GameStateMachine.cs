using __ProjectMain.Scripts.Managers.GameStateMachine.States;
using __ProjectMain.Scripts.Patterns.State;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Managers.GameStateMachine
{
    public class GameStateMachine : Patterns.State.StateMachine
    {
        public MainMenuState MainMenuState { get; } = new();
        public IngameState IngameState { get; } = new();
        public LevelEditorState LevelEditorState { get; } = new();
        public LevelEditorMenuState LevelEditorMenuState { get; } = new();

        public void OnEsc(InputAction.CallbackContext ctx)  {
            if(ctx.performed) ((IGameState)currentState).OnEsc(ctx);
        }
    }
}