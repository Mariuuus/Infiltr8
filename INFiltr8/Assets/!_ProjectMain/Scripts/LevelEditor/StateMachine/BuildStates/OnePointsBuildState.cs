using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
{
    public abstract class OnePointsBuildState : BuildState
    {
        public Vector3Int LatestCellClicked = new Vector3Int(-1,-1,-1);

        public override void OnEsc(InputAction.CallbackContext ctx)
        {
            base.OnClick(ctx);
            if (ctx.performed)
            {
                LatestCellClicked = new Vector3Int(-1,-1,-1); }
        }
    }
}