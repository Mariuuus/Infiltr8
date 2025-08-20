using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;


namespace __ProjectMain.Scripts.UI
{
    public class Settings : MonoBehaviour, IClickableMenuElement
    { 
        [SerializeField] private Vector3 normalScale; 
        [SerializeField] private Vector3 hoverScale;

        public GameObject settingsMenu;
        
        public void Awake()
        {
            settingsMenu.SetActive(false);
        }
        
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
            MainMenuManager.Instance.currentState = State.Settings;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.settingsCamera);

            settingsMenu.SetActive(true);
        }

        public void OnUnclick()
        {
            StartCoroutine(DelayHide());
        }

        private IEnumerator DelayHide()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            settingsMenu.SetActive(false);
        }
    }
}
