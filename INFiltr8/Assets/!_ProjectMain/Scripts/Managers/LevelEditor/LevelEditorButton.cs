using __ProjectMain.Scripts.LevelEditor.StateMachine;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class LevelEditorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ISelectableState _selectableState;
        public Image image;
        public void OnClick()
        {
            LevelEditorManager.Instance.SelectEditorState(_selectableState);
        }
        
        public void Init(ISelectableState selectableState)
        {
            this._selectableState = selectableState;
            image.sprite = selectableState.GetIcon();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            LevelEditorManager.Instance.OnPointerEnterUI();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LevelEditorManager.Instance.OnPointerExitUI();
        }
    }
}