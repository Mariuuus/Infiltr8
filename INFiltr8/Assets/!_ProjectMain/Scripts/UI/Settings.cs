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

            StartCoroutine(DelayMixerCanvas());
        }

        private IEnumerator DelayMixerCanvas()
        {
            yield return new WaitForSecondsRealtime(1);
            settingsMenu.SetActive(true);
        }
        
        public void OnUnclick()
        {
            //do something here lol
        }
    }
}
