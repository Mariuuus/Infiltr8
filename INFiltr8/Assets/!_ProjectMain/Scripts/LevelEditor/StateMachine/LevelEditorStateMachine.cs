using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using __ProjectMain.Scripts.LevelEditor.StateMachine.EditStates;
using __ProjectMain.Scripts.Utilities.Exceptions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public class LevelEditorStateMachine : Patterns.State.StateMachine
    {
        public WallBuildState WallBuildState { get; } = new();
        public FireWallBuildState FireWallBuildState { get; } = new();
        public SpectateState SpectateState { get; } = new();
        public ChangePointsState ChangePointsState { get; } = new();
        public SpawnPointBuildState SpawnPointBuildState { get; } = new();
        public GoalBuildState GoalBuildState { get; } = new();
        public LaptopBuildState LaptopBuildState { get; } = new();
        
        public OnlyPlayerDoorBuildState OnlyPlayerDoorBuildState { get; } = new ();

        public DeleteComponentsState DeleteComponentsState { get; } = new();
        public AdjustComponentState AdjustComponentState { get; } = new();
        public PortBuildState PortBuildState { get; } = new();
        public DialogAreaBuildState DialogAreaBuildState { get; } = new();
        public CollectableBuildState CollectableBuildState { get; } = new();
        
        /*
         * This works for overwriting the "normal" State Machine
         */
        public void OnClick(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            try
            {
                ((ILevelEditorState)currentState).OnClick(ctx);
            }
            catch (InvalidLevelEditorActionException e)
            {
                //TODO: display Errors!
                Debug.LogError(e.Message);
            }
        }
        public void OnEsc(InputAction.CallbackContext ctx)
        {
            if(ctx.performed) ((ILevelEditorState)currentState).OnEsc(ctx);
        }
    }
}