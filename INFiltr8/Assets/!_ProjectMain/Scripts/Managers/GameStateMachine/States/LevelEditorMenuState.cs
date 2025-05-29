using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.GameStateMachine.States
{
    public class LevelEditorMenuState : IGameState
    {
        public void Enter()
        {
            SceneManager.LoadScene("LevelEditorMenu");

        }
        public void Exit() {}

        public void Update() {}

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine.MainMenuState);
        }
    }
}