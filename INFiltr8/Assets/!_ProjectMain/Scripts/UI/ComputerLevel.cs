using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using LevelType = __ProjectMain.Scripts.LevelEditor.LevelType;

namespace __ProjectMain.Scripts.UI
{
    public class ComputerLevel : MonoBehaviour, IClickableMenuElement
    {
        [SerializeField] private Vector3 normalScale;
        [SerializeField] private Vector3 hoverScale;
        
        [SerializeField] private GameObject previewLevelScreen;
        public GameObject levelsScreen;
        
        [SerializeField] private TMP_Text levelNumber;
        [SerializeField] private TMP_Text levelName;
        [SerializeField] private TMP_Text bestTimeObj;
        [SerializeField] private GameObject bestTime;
        [SerializeField] private GameObject timeIcon;
        
        private LevelData _selectedLevel;
        private int _levelNumb;

        private void Start()
        {
            previewLevelScreen.SetActive(false);
            //levelsScreen.SetActive(MainMenuManager.Instance.currentState == State.LevelSelect);
        }

        public void OnHoverStart()
        {
            transform.localScale = hoverScale;
        }

        public void OnHoverEnd()
        {
            transform.localScale = normalScale;
        }

        public void OnPreview(LevelData levelData,  int levelNumb)
        {
            if (levelNumb-1 > GameDataManager.Instance.ProgressLevel()) return;
            _selectedLevel = levelData;
            _levelNumb = levelNumb-1;
            this.levelNumber.text = "Level " + levelNumb + ":";
            this.levelName.text = levelData.levelName;
            bestTime.SetActive(levelData.levelType != LevelType.GetData);
            timeIcon.SetActive(levelData.levelType != LevelType.GetData);
            bestTimeObj.text = $"todo";
            levelsScreen.SetActive(false);
            previewLevelScreen.SetActive(true);
        }

        public void OnBackClick()
        {
            levelsScreen.SetActive(true);
            previewLevelScreen.SetActive(false);
        }

        public void OnPlay()
        {
            LevelLoaderManager.Instance?.LoadLevel(_selectedLevel, _levelNumb);
        }

        public void OnClick()
        {
            MainMenuManager.Instance.currentState = State.LevelSelect;
            levelsScreen.SetActive(MainMenuManager.Instance.currentState == State.LevelSelect);
            CameraManager.Instance.ChangeToCamera(CameraManager.Instance.levelSelectorCamera);
            MainMenuManager.Instance.backButtonInMainMenuRef.Show();
        }

        public void OnUnclick()
        {
            levelsScreen.SetActive(false);
            previewLevelScreen.SetActive(false);
        }
    }
}