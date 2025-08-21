using System;
using System.Collections.Generic;
using System.IO;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.LevelBrowserMenu
{
    public class LevelBrowserManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject uiElement;
        [SerializeField] private GameObject levelsSearchContainer;
        [SerializeField] private GameObject levelSearchRowPrefab;
        
        [SerializeField] private GameObject localLevelsSearchContainer;
        [SerializeField] private GameObject localLevelSearchRowPrefab;
        
        
        [Header("Render Inspector Elements")]
        [SerializeField] private TMP_Text levelNameText;
        [SerializeField] private TMP_Text authorNameText;
        [SerializeField] private TMP_Text uploadDateText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button saveLocallyButton;

        [Header("Create Form Elements")]
        [SerializeField] private TMP_InputField search;
        [SerializeField] private TMP_InputField searchLocally;
        [SerializeField] private TMP_InputField authorStr;


        private string _selectedSearchLevelID;
        private LevelData _selectedLevelData;
        private string _selectedLocalLevelID;
        private LevelData _selectedLocalLevelData;
        
        private List<LevelData> _currentLocalLevels;


        private async void RenderLevels()
        {
            for (int i = levelsSearchContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(levelsSearchContainer.transform.GetChild(i).gameObject);
            }

            var pagedResult = await LevelApi.GetLevelsAsync(page: 1, pageSize: 10, search: search.text ?? "");
            if (pagedResult == null || pagedResult.Items == null) return;

            for (int i = 0; i < pagedResult.Items.Count; i++)
            {
                var levelSummary = pagedResult.Items[i];
                var newObj = Instantiate(levelSearchRowPrefab, levelsSearchContainer.transform);
                var rowComponent = newObj.GetComponent<LevelBrowserLevelRow>();
                if (rowComponent != null)
                {
                    rowComponent.Init(levelSummary.Id, levelSummary.Name, levelSummary.Author, this);
                }
            }
        }

        private void Start()
        {
            saveLocallyButton.interactable = false;
            playButton.interactable = false;
        }

        public void OnSearch()
        {
            string searchStr = search.text;
            RenderLevels();
        }

        public async void OnSelectLevel(string id)
        {
            var fullLevel = await LevelApi.GetLevelByIdAsync(id);
            if (fullLevel == null) return;
            _selectedSearchLevelID = fullLevel.Id;
            levelNameText.text = fullLevel.Name;
            authorNameText.text = fullLevel.Author;
            uploadDateText.text = fullLevel.UploadDate.ToShortDateString();
            try
            {
                LevelData levelData = LevelDataUtils.DeserializeFromString(fullLevel.Content);
                if (levelData != null)
                {
                    _selectedLevelData = levelData;
                    saveLocallyButton.interactable = true;
                    playButton.interactable = true;
                }
                else
                {
                    throw new FileLoadException("Could not load LevelData");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("could not be deserialized into a LevelData" + e);
                saveLocallyButton.interactable = false;
                playButton.interactable = false;
            }
            
        }

        private void RenderLocalLevels()
        {
            for (int i = localLevelsSearchContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(localLevelsSearchContainer.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < _currentLocalLevels.Count; i++)
            {
                var newObj = Instantiate(localLevelSearchRowPrefab, localLevelsSearchContainer.transform);
                newObj.GetComponent<LevelBrowserLocalLevelRow>().Init(_currentLocalLevels[i].levelName, _currentLocalLevels[i], _selectedLocalLevelData==_currentLocalLevels[i], this);
            }
        }

        public void OnSearchLocalLevels(string newString)
        {
            _currentLocalLevels = LevelDataUtils.SearchTop5(LevelDataUtils.GetAvailableLocalLevels(), newString);
            RenderLocalLevels();
        }

        public void OnSaveLevelLocally()
        {
            
        }

        public void OnPlayOnlineLevel()
        {
            if (_selectedLevelData == null) return;
            LevelLoaderManager.Instance.LoadLocalLevel(_selectedLevelData);
        }

        public void OnSelectLocalLevel(LevelData levelData)
        {
            _selectedLocalLevelData = levelData;
            RenderLocalLevels();
        }

        public async void OnUploadLocalLevel()
        {
            if (_selectedLocalLevelData == null)
            {
                Debug.LogError("No local level selected to upload.");
                return;
            }
            if (string.IsNullOrWhiteSpace(authorStr.text))
            {
                Debug.LogError("Author cannot be empty.");
                return;
            }
            try
            {
                string serializedContent = LevelDataUtils.SerializeToString(_selectedLocalLevelData);

                string levelName = _selectedLocalLevelData.levelName;

                var createdLevel = await LevelApi.CreateLevelAsync(
                    name: levelName,
                    author: authorStr.text,
                    content: serializedContent
                );

                if (createdLevel != null)
                {
                    Debug.Log($"Level '{createdLevel.Name}' uploaded successfully! Id: {createdLevel.Id}");
                }
                else
                {
                    Debug.LogError("Failed to upload level.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while uploading level: {e}");
            }
        }
    }
}