using System.Collections;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;


namespace __ProjectMain.Scripts.UI
{
    public class Exit : MonoBehaviour, IClickableMenuElement
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
            MainMenuManager.Instance.currentState = State.Exit;
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.exitCamera);

            StartCoroutine(DelayExit());
        }

        private IEnumerator DelayExit()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Application.Quit();
        }

        public void OnUnclick()
        {
            // insert CODE
        }
        
    }
}
