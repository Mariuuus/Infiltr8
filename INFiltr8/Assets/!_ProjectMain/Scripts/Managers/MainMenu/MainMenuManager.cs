using System;
using __ProjectMain.Scripts.UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Managers.MainMenu
{

    public enum State
    {
        Intro, Overview, LevelEditor, LevelSelect
    }
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance { get; private set; }
        public bool Playing{ get; private set; }
        public State currentState =  State.Overview;
        
        private GameObject _hitObject;
        
        
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != _hitObject)
                {
                    IClickableMenuElement clickableMenuElement = hitObject.GetComponent<IClickableMenuElement>();
                    
                    if (clickableMenuElement != null)
                    {   
                        if (Input.GetMouseButtonDown(0))
                        {
                            clickableMenuElement.OnClick();
                            clickableMenuElement.OnHoverEnd();
                        }
                        else
                        {
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
                else if (Input.GetMouseButtonDown(0))
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