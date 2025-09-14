using System;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class BackButtonInMainMenu : MonoBehaviour, IControlsListener
    {
        [SerializeField] private Sprite controllerSprite;
        [SerializeField] private Sprite keyboardSprite;
        [SerializeField] private GameObject obj;
        [SerializeField] private Image img;

        private void Start()
        {
            ControlsManager.Instance.AddSubscriber(this);
            Debug.Log(MainMenuManager.Instance.currentState + " is this state!");
            if (MainMenuManager.Instance.currentState != State.Overview)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        
        private void OnDestroy() => ControlsManager.Instance.RemoveSubscriber(this);

        public void Show() => obj.SetActive(true); 
        public void Hide() => obj.SetActive(false); 
        public void OnChange()
        {
            if (ControlsManager.Instance.usedDevice == Device.Keyboard)
            {
                img.sprite = keyboardSprite;
            } else if (ControlsManager.Instance.usedDevice == Device.Gamepad)
            {
                img.sprite = controllerSprite;
            }
            
        }
    }
}