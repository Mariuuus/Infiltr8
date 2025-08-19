using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.UI.LevelEditorMenu;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.UI
{
    public class LevelEditor : MonoBehaviour, IClickableMenuElement
    {
        [SerializeField] private Vector3 normalScale;
        [SerializeField] private Vector3 hoverScale;
        
        [SerializeField] private LevelEditorManager  levelEditorManager;
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
            MainMenuManager.Instance.currentState = State.LevelEditor;
            levelEditorManager.Show();
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.levelEditorCamera);
        }

        public void OnUnclick()
        {
            levelEditorManager.Hide();
            //do something here lol
        }
    }
}