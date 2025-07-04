using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.UI
{
    public class ComputerLevel : MonoBehaviour, IClickableMenuElement
    {
        [SerializeField] private Vector3 normalScale;
        [SerializeField] private Vector3 hoverScale;
        public void OnHoverStart()
        {
            transform.localScale = hoverScale;
        }

        public void OnHoverEnd()
        {
            transform.localScale = normalScale;

        }

        public void OnClick()
        {
            MainMenuManager.Instance.currentState = State.LevelSelect;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.levelSelectorCamera);
        }

        public void OnUnclick()
        {
            //do something here lol
        }
    }
}