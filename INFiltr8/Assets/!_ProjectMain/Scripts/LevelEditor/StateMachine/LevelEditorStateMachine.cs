using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public class LevelEditorStateMachine : Patterns.State.StateMachine
    {
        public WallBuildState WallBuildState { get; }
        public SpectateState SpectateState { get; }
        public ChangePointsState ChangePointsState { get; }

        public LevelEditorStateMachine()
        {
            WallBuildState = new WallBuildState();
            SpectateState = new SpectateState();
            ChangePointsState = new ChangePointsState();
            
        }
        
        /*
         * This works for overwriting the "normal" State Machine
         */
        public void OnClick(InputAction.CallbackContext ctx) {
            try
            {
                ((ILevelEditorState)currentState).OnClick(ctx);
            }
            catch (InvalidLevelEditorException e)
            {
                //TODO: display Errors!
                Debug.LogError(e.Message);
            }
        }
        public void OnEsc(InputAction.CallbackContext ctx) => ((ILevelEditorState)currentState).OnEsc(ctx);
    }
}