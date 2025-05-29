using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.GameStateMachine.States
{
    public class MainMenuState : IGameState
    {
        public void Enter()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void Exit() {}

        public void Update() {}

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            //nothing here!
        }
    }
}