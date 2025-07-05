using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace __ProjectMain.Scripts.Managers.MainMenu
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        public CinemachineCamera overviewCamera;
        public CinemachineCamera levelEditorCamera;
        public CinemachineCamera levelSelectorCamera;
        public PlayableDirector introDirector;
        
        private CinemachineCamera _currentCamera;
        
        
        public bool Playing { get; private set; }
        
        
        public void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            DeactivateCameras();
            _currentCamera = overviewCamera;
            ChangeToCamera(overviewCamera);
        }
        
        public void GameStartSequence() => PlayIntro();

        
        public void ChangeToCamera(CinemachineCamera nextCamera)
        {
            if (Playing) return;
            _currentCamera.gameObject.SetActive(false);
            _currentCamera = nextCamera;
            _currentCamera.gameObject.SetActive(true);
        }

        private void DeactivateCameras()
        {
            overviewCamera.gameObject.SetActive(false);
            levelEditorCamera.gameObject.SetActive(false);
            levelSelectorCamera.gameObject.SetActive(false);
        }

        public void PlayIntro()
        {
            introDirector.Play();
            if (MainMenuManager.Instance)
            {
                MainMenuManager.Instance.currentState =  State.Intro;
            }
        }
        
        public void StopIntro()
        {
            introDirector.Stop();
            MainMenuManager.Instance.currentState =  State.Overview;
            ChangeToCamera(overviewCamera);
        }

        private void Update()
        {
            if (Math.Abs(introDirector.duration - introDirector.time) < 0.01f)            
            {
                Playing = false;
                if(MainMenuManager.Instance.currentState == State.Intro) MainMenuManager.Instance.currentState =  State.Overview;
            }
        }
    }
}