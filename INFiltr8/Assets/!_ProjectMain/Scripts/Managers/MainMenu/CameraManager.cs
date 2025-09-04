using System;
using __ProjectMain.Scripts.UI;
using __ProjectMain.Scripts.UI.LevelBrowserMenu;
using __ProjectMain.Scripts.UI.LevelEditorMenu;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

namespace __ProjectMain.Scripts.Managers.MainMenu
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        public CinemachineCamera overviewCamera;
        public CinemachineCamera levelEditorCamera;
        public CinemachineCamera levelSelectorCamera;
        public CinemachineCamera settingsCamera;
        public CinemachineCamera exitCamera;
        public CinemachineCamera onlineLevelCamera;
        public CinemachineCamera achievementCamera;
        public PlayableDirector introDirector;
        
        private CinemachineCamera _currentCamera;
        
        
        public bool Playing { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Start()
        {
            Init();
        }
        
        public void Init()
        {
            DeactivateCameras();

            switch (LevelLoaderManager.Instance.currentLevelType)
            {
                case LevelType.Online:
                    FindFirstObjectByType<LevelBrowserManager>().Show();
                    MainMenuManager.Instance.SetHitObj(FindFirstObjectByType<LevelOnlineState>().gameObject);
                    _currentCamera = onlineLevelCamera;
                    ChangeToCamera(onlineLevelCamera);
                    MainMenuManager.Instance.currentState = State.OnlineLevel;
                    break;
                case LevelType.Regular:
                    FindFirstObjectByType<ComputerLevel>().levelsScreen.SetActive(true);
                    MainMenuManager.Instance.SetHitObj(FindFirstObjectByType<ComputerLevel>().gameObject);
                    _currentCamera = levelSelectorCamera;
                    ChangeToCamera(levelSelectorCamera);
                    MainMenuManager.Instance.currentState = State.LevelSelect;
                    break;
                case LevelType.LevelEditor:
                    FindFirstObjectByType<LevelEditorManager>().Show();
                    _currentCamera = levelEditorCamera;
                    ChangeToCamera(levelEditorCamera);
                    MainMenuManager.Instance.currentState = State.LevelEditor;
                    MainMenuManager.Instance.SetHitObj(FindFirstObjectByType<UI.LevelEditor>().gameObject);
                    break;
                default:
                    _currentCamera = overviewCamera;
                    ChangeToCamera(overviewCamera);
                    MainMenuManager.Instance.currentState = State.Overview;
                    break;
            }
        }
        
        public void GameStartSequence() => PlayIntro();

        
        public void ChangeToCamera(CinemachineCamera nextCamera)
        {
            Debug.Log("Changing from "+ _currentCamera +" to camera" + nextCamera);
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
            settingsCamera.gameObject.SetActive(false);
            exitCamera.gameObject.SetActive(false);
            onlineLevelCamera.gameObject.SetActive(false);
            achievementCamera.gameObject.SetActive(false);
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
                if(MainMenuManager.Instance.currentState == State.Intro)
                {
                    Debug.Log("Reset Manager");
                    MainMenuManager.Instance.currentState = State.Overview;
                    //GameDataManager.Instance.WatchedIntro();
                }
            }
        }
    }
}