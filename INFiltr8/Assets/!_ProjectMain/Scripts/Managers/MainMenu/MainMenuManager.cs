using System;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Managers.MainMenu
{

    public enum State
    {
        Intro, Overview, LevelEditor, LevelSelect, Settings, Exit, OnlineLevel, Achievements
    }
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance { get; private set; }
        public bool Playing{ get; private set; }
        public State currentState =  State.Overview;
        
        private GameObject _hitObject;
        
        [SerializeField] private VirtualMouseInput virtualMouseInput;
        
        [SerializeField] private AudioClip menuHoverSound;
        [SerializeField] private AudioClip menuSelectSound;
        
        
        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

        }

        void Update()
        {

            if (currentState != State.Overview) return;
            Ray ray;
            if (ControlsManager.Instance.usedDevice == Device.Keyboard)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(new Vector3(virtualMouseInput.virtualMouse.position.x.value, virtualMouseInput.virtualMouse.position.y.value, 0));
            }
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != _hitObject)
                {
                    IClickableMenuElement clickableMenuElement = hitObject.GetComponent<IClickableMenuElement>();
                    
                    if (clickableMenuElement != null)
                    {   
                        if (ControlsManager.Instance.usedDevice == Device.Keyboard && Input.GetMouseButtonDown(0) ||
                            ControlsManager.Instance.usedDevice == Device.Gamepad &&
                            virtualMouseInput.leftButtonAction.action.WasPerformedThisFrame())
                        {
                            clickableMenuElement.OnClick();
                            // SfxManager.instance.PlaySfxClip(menuSelectSound, 1f);
                            clickableMenuElement.OnHoverEnd();
                        }
                        else
                        {
                            SfxManager.instance.PlaySfxClip(menuHoverSound, 1f);
                            clickableMenuElement.OnHoverStart();
                        }
                    }

                    if (_hitObject)
                    {
                        IClickableMenuElement oldClickableMenuElement =
                            _hitObject.GetComponent<IClickableMenuElement>();
                        if (oldClickableMenuElement != null) oldClickableMenuElement.OnHoverEnd();
                    }

                    _hitObject = hitObject;
                }
                else if (ControlsManager.Instance.usedDevice == Device.Keyboard && Input.GetMouseButtonDown(0) ||
                         ControlsManager.Instance.usedDevice == Device.Gamepad &&
                         virtualMouseInput.leftButtonAction.action.WasPerformedThisFrame())
                {
                    _hitObject.GetComponent<IClickableMenuElement>()?.OnClick();
                    _hitObject.GetComponent<IClickableMenuElement>()?.OnHoverEnd();
                    
                }
            }
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (currentState == State.Overview) return;
            if (currentState == State.Intro)
            {
                CameraManager.Instance.StopIntro();
                return;
            }
            _hitObject?.GetComponent<IClickableMenuElement>()?.OnUnclick();
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.overviewCamera);
            currentState = State.Overview;
        }
    }
}