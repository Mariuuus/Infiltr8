using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.GameStateMachine.States
{
    public class IngameState : IGameState
    {
        public void Enter()
        {
            SceneManager.LoadScene("LevelLoader");
        }
        public void Exit() {}

        public void Update() {}

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine.LevelEditorMenuState);
        }
    }
}