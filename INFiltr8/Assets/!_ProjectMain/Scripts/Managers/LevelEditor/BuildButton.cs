using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class BuildButton : MonoBehaviour
    {
        private BuildState _buildState;
        public Image image;
        public void OnClick()
        {
            LevelEditorManager.Instance.ChangeBuildMode(_buildState);
        }
        
        public void Init(BuildState buildState)
        {
            this._buildState = buildState;
            image.sprite = _buildState.GetIcon();
        }
    }
}