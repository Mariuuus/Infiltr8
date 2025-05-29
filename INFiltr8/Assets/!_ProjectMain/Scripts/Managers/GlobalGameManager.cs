using System;
using __ProjectMain.Scripts.Managers.GameStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Managers
{
    public class GlobalGameManager : MonoBehaviour
    {
        public GameStateMachine.GameStateMachine GameStateMachine { get; private set; }
        public static GlobalGameManager Instance { private set; get; }

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            GameStateMachine = new GameStateMachine.GameStateMachine();
            GameStateMachine.ChangeState(GameStateMachine.MainMenuState);
        }
        
        public void OnEsc(InputAction.CallbackContext ctx)
        {
            GameStateMachine.OnEsc(ctx);
        }
    }
}