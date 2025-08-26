using System.Collections;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Managers.MainMenu;
using UnityEngine;


namespace __ProjectMain.Scripts.UI
{
    public class Exit : MonoBehaviour, IClickableMenuElement
    { 
        [SerializeField] private Vector3 normalScale; 
        [SerializeField] private Vector3 hoverScale;
        
        [SerializeField] private AudioClip menuSelectSound;
        
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
            
            // SfxManager.instance.PlaySfxClip(menuSelectSound, 1f);

            StartCoroutine(DelayExit());
        }

        private IEnumerator DelayExit()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Application.Quit();
            Debug.Log("Quit");
        }

        public void OnUnclick()
        {
            // insert CODE
        }
        
    }
}
