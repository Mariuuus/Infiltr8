using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class LevelOnlineState : MonoBehaviour, IClickableMenuElement
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
            MainMenuManager.Instance.currentState = State.OnlineLevel;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.onlineLevelCamera);
        }

        public void OnUnclick()
        {
            // insert CODE
        }
    }

}