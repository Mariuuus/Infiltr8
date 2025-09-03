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
        Intro,
        Overview,
        LevelEditor,
        LevelSelect,
        Settings,
        Exit,
        OnlineLevel,
        Achievements
    }

    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance { get; private set; }
        public State currentState = State.Overview;
        
        private GameObject _hitObject;
        private GameObject _hitObjectDecoration;

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

        private void Start()
        {
            if(GameDataManager.Instance.gameData.introDone) MusicManager.Instance.PlayMainMenuMusic();
        }

        public void SetHitObj(GameObject hitObject) =>  _hitObject = hitObject;

        void Update()
        {
            if (currentState == State.Intro) return;
            if (currentState == State.Overview)
            {
                Ray ray;
                if (ControlsManager.Instance.usedDevice == Device.Keyboard)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(new Vector3(virtualMouseInput.virtualMouse.position.x.value,
                        virtualMouseInput.virtualMouse.position.y.value, 0));
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
                        // check if currently hovered object is ClickableMenuElement
                        var menuElement = _hitObject.GetComponent<IClickableMenuElement>();
                        if (menuElement != null)
                        {
                            SfxManager.instance.PlaySfxClip(menuSelectSound,1f);
                        }
                        _hitObject.GetComponent<IClickableMenuElement>()?.OnClick();
                        _hitObject.GetComponent<IClickableMenuElement>()?.OnHoverEnd();
                    }
                }
            }
            else
            {
                //check for decoration
                Ray ray;
                if (ControlsManager.Instance.usedDevice == Device.Keyboard)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(new Vector3(virtualMouseInput.virtualMouse.position.x.value,
                        virtualMouseInput.virtualMouse.position.y.value, 0));
                }

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != _hitObjectDecoration)
                    {
                        IClickableMenuDecoration clickableMenuElement = hitObject.GetComponent<IClickableMenuDecoration>();

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
                                //SfxManager.instance.PlaySfxClip(menuHoverSound, 1f);
                                clickableMenuElement.OnHoverStart();
                            }
                        }

                        if (_hitObjectDecoration)
                        {
                            IClickableMenuDecoration oldClickableMenuElement =
                                _hitObjectDecoration.GetComponent<IClickableMenuDecoration>();
                            if (oldClickableMenuElement != null) oldClickableMenuElement.OnHoverEnd();
                        }

                        _hitObjectDecoration = hitObject;
                    }
                    else if (ControlsManager.Instance.usedDevice == Device.Keyboard && Input.GetMouseButtonDown(0) ||
                             ControlsManager.Instance.usedDevice == Device.Gamepad &&
                             virtualMouseInput.leftButtonAction.action.WasPerformedThisFrame())
                    {
                        _hitObjectDecoration.GetComponent<IClickableMenuDecoration>()?.OnClick();
                        _hitObjectDecoration.GetComponent<IClickableMenuDecoration>()?.OnHoverEnd();
                    }
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

            if (currentState != State.Exit)
            {
                _hitObject?.GetComponent<IClickableMenuElement>()?.OnUnclick();
                CameraManager.Instance.ChangeToCamera(CameraManager.Instance.overviewCamera);
                currentState = State.Overview;
            }
        }
    }
}