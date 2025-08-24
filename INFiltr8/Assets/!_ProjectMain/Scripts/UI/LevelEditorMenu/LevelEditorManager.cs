using System;
using System.IO;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.UI.LevelEditorMenu
{
    public class LevelEditorManager: MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject uiElement;
        [SerializeField] private GameObject levelsContainer;
        [SerializeField] private GameObject levelRowPrefab;
        
        [Header("Create Form Elements")]
        [SerializeField] private TMP_InputField heightInput;
        [SerializeField] private TMP_InputField widthInput;
        [SerializeField] private TMP_InputField nameInput;

        public void RenderLevels()
        {
            for (int i = levelsContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(levelsContainer.transform.GetChild(i).gameObject);
            }

            var levels = LevelDataUtils.GetAvailableLocalLevels();
            for (int i = 0; i < levels.Count; i++)
            {
                var newObj = Instantiate(levelRowPrefab, levelsContainer.transform);
                newObj.GetComponent<LevelEditorLevelRow>().Init(i,  levels[i], LevelDataUtils.ReceiveFileName(levels[i].levelName), this);
            }
        }

        private void Start()
        {
            //Hide();
        }

        public void Show()
        {
            uiElement.SetActive(true);
            RenderLevels();
        }

        public void Hide()
        {
            uiElement.SetActive(false);
        } 
        
        public void OnPlay(LevelData levelData)
        {
            LevelLoaderManager.Instance.LoadLocalLevel(levelData);
        }

        public void OnEdit(LevelData levelData)
        {
            LevelLoaderManager.Instance.LoadLocalLevelEdit(levelData);
        }
        
        public void OnDelete(string path)
        {
            LevelDataUtils.RemoveLocalLevel(path);
            RenderLevels();
        }

        public void OnCreate()
        {
            CreateNewLevel(nameInput.text, Int32.Parse(heightInput.text), Int32.Parse(widthInput.text));
            RenderLevels();
        }

        private bool CreateNewLevel(string name, int height, int width)
        {
            var newLevel = new LevelData(name, height, width);
            try
            {
                LevelDataUtils.SaveFile(newLevel);
            }
            catch (FileLoadException)
            {
                return false;
            }
            
            return true;
        }
    }
}