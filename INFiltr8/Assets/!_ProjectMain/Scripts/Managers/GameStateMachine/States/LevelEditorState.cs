using __ProjectMain.Scripts.Managers.LevelEditor;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.GameStateMachine.States
{
    public class LevelEditorState : IGameState
    {
        public void Enter()
        {
            SceneManager.LoadScene("LevelEditor");

        }
        public void Exit() {}

        public void Update() {}

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            if (LevelEditorManager.Instance.isSpecting)
            {
                GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine.LevelEditorMenuState);
            }
        }
    }
}