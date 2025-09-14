using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class AchievementsState : MonoBehaviour, IClickableMenuElement
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
            Debug.Log("AchievementsState.OnClick()");
            MainMenuManager.Instance.currentState = State.Achievements;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.achievementCamera);
            MainMenuManager.Instance.backButtonInMainMenuRef.Show();
        }

        public void OnUnclick()
        {
            // insert CODE
        }
    }
}