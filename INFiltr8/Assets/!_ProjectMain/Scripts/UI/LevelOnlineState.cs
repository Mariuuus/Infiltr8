using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.UI.LevelBrowserMenu;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class LevelOnlineState : MonoBehaviour, IClickableMenuElement
    {
        [SerializeField] private Vector3 normalScale;
        [SerializeField] private Vector3 hoverScale;
        [SerializeField] private LevelBrowserManager levelBrowserManager;


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
            StartCoroutine(DelayUI());
        }

        private IEnumerator DelayUI()
        {
            yield return new WaitForSecondsRealtime(1f);
            levelBrowserManager.Show();
        }

        public void OnUnclick()
        {
            levelBrowserManager.Hide();
        }
    }

}