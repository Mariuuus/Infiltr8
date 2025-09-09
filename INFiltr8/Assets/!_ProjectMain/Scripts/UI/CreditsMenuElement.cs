using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class CreditsMenuElement : MonoBehaviour, IClickableMenuElement
    {
        [SerializeField] private Vector3 normalScale = Vector3.one;
        [SerializeField] private Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);

        [SerializeField] private GameObject ui;

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
            StopCoroutine(nameof(DelayExit));
            MainMenuManager.Instance.currentState = State.Credits;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.creditsCamera);
            ui.SetActive(true);
        }

        private IEnumerator DelayExit()
        {
            yield return new WaitForSecondsRealtime(.6f);
            ui.SetActive(false);
        }

        public void OnUnclick()
        {
            StartCoroutine(nameof(DelayExit));
        }
    }
}