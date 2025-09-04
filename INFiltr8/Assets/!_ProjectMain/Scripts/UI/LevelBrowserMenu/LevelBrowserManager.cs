using System;
using System.Collections.Generic;
using System.IO;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers;
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
        [SerializeField] private GameObject loggingWindow;
        [SerializeField] private GameObject registerWindow;
        [SerializeField] private GameObject uploadWindow;
        [SerializeField] private GameObject levelsSearchContainer;
        [SerializeField] private GameObject levelSearchRowPrefab;
        
        [SerializeField] private GameObject localLevelsSearchContainer;
        [SerializeField] private GameObject localLevelSearchRowPrefab;
        
        [SerializeField] private GameObject onlineOwnLevelContainer;
        [SerializeField] private GameObject onlineOwnLevelPrefab;

        
        [Header("Render Inspector Elements")]
        [SerializeField] private TMP_Text levelNameText;
        [SerializeField] private TMP_Text authorNameText;
        [SerializeField] private TMP_Text uploadDateText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button saveLocallyButton;
        [SerializeField] private Button nextPageButton;
        [SerializeField] private Button prevPageButton;
        [SerializeField] private TMP_Text pageIndicator;

        
        [Header("Create Form Elements")]
        [SerializeField] private TMP_InputField search;
        [SerializeField] private TMP_InputField searchLocally;
        [SerializeField] private TMP_InputField authorStr;
        
        [Header("Login Element")]
        [SerializeField] private TMP_InputField usernameLogin;
        [SerializeField] private TMP_InputField passwordLogin;

        [Header("Register Element")]
        [SerializeField] private TMP_InputField usernameRegister;
        [SerializeField] private TMP_InputField passwordRegister;

        private string _selectedSearchLevelID;
        private LevelData _selectedLevelData;
        private string _selectedLocalLevelID;
        private LevelData _selectedLocalLevelData;
        private List<LevelData> _currentLocalLevels;
        private int maxPage = -1;
        private int currentPage = 1;
        
        private async void RenderLevels()
        {

            prevPageButton.interactable = false;
            nextPageButton.interactable = false;
            for (int i = levelsSearchContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(levelsSearchContainer.transform.GetChild(i).gameObject);
            }

            var pagedResult = await LevelApi.GetLevelsAsync(page: currentPage, pageSize: 10, search: search.text ?? "");
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
            
            currentPage = pagedResult.Page;
            maxPage = pagedResult.TotalPages;
            
            prevPageButton.interactable = (currentPage > 1);
            nextPageButton.interactable = (maxPage > currentPage);
            
            pageIndicator.text = currentPage+"/"+maxPage;
        }

        public void NextPage()
        {
            currentPage++;
            RenderLevels();
        }

        public void PrevPage()
        {
            currentPage--;
            RenderLevels();
        }
        
        public void Show()
        {
            uiElement.SetActive(true);
            OnSearchLocalLevels("");
            OnSearch();
        }

        public void Hide()
        {
            uiElement.SetActive(false);
            loggingWindow.SetActive(false);
            registerWindow.SetActive(false);
            uploadWindow.SetActive(false);
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

        public async void OnDeleteOnlineLevel(string id)
        {
            var success = await LevelApi.DeleteLevelAsync(id, GameDataManager.Instance.username, GameDataManager.Instance.password);
            LoadOnlineOwnLevels();
        }

        public async void OnLogin()
        {
            GameDataManager.Instance.loggedIn = await LevelApi.GetAuthState(
                username: passwordLogin.text,
                password: passwordLogin.text
            );
            if (GameDataManager.Instance.loggedIn)
            {
                GameDataManager.Instance.username = usernameLogin.text;
                GameDataManager.Instance.password = passwordLogin.text;
                usernameLogin.text = "";
                passwordLogin.text = "";
                loggingWindow.SetActive(false);
                uploadWindow.SetActive(true);
                LoadOnlineOwnLevels();
            }
            else
            {
                
            }
        }
        public async void OnRegister()
        {
            GameDataManager.Instance.loggedIn = await LevelApi.CreateUser(
                username: usernameRegister.text,
                password: passwordRegister.text
            );
            if (GameDataManager.Instance.loggedIn)
            {
                GameDataManager.Instance.username = usernameRegister.text;
                GameDataManager.Instance.password = passwordRegister.text;
                usernameRegister.text = "";
                passwordRegister.text = "";
                registerWindow.SetActive(false);
                uploadWindow.SetActive(true);   
                LoadOnlineOwnLevels();
            }
            else
            {
                
            }
        }
        
        public void SwitchToLogin()
        {
            loggingWindow.SetActive(true);
            registerWindow.SetActive(false);   
        }
        
        public void SwitchToRegister()
        {
            loggingWindow.SetActive(false);
            registerWindow.SetActive(true);   
        }

        public void OnLogout()
        {
            GameDataManager.Instance.loggedIn = false;
            GameDataManager.Instance.username = "";
            GameDataManager.Instance.password = "";
            Hide();
            Show();
        }
        
        public void GoBack()
        {
            Hide();
            Show();
        }
        

        public void OnUpload()
        {
            uiElement.SetActive(false);
            if (!GameDataManager.Instance.loggedIn)
            {
                loggingWindow.SetActive(true);
            }
            else
            {
                uploadWindow.SetActive(true);
                LoadOnlineOwnLevels();
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
            _currentLocalLevels = LevelDataUtils.SearchTopN(LevelDataUtils.GetAvailableLocalLevels(), newString, 20);
            RenderLocalLevels();
        }

        public void OnSaveLevelLocally()
        {
            
        }

        public void OnPlayOnlineLevel()
        {
            if (_selectedLevelData == null) return;
            LevelLoaderManager.Instance.LoadLocalLevel(_selectedLevelData, true);
        }

        public void OnUploadLocalLevel(LevelData levelData)
        {
            _selectedLocalLevelData = levelData;
            RenderLocalLevels();
            OnUploadLocalLevel();
        }

        public async void OnUploadLocalLevel()
        {
            if (_selectedLocalLevelData == null)
            {
                Debug.LogError("No local level selected to upload.");
                return;
            }
            try
            {
                string serializedContent = LevelDataUtils.SerializeToString(_selectedLocalLevelData);

                string levelName = _selectedLocalLevelData.levelName;

                var createdLevel = await LevelApi.CreateLevelAsync(
                    name: levelName,
                    content: serializedContent,
                    username:GameDataManager.Instance.username,
                    password:GameDataManager.Instance.password
                );

                if (createdLevel != null)
                {
                    Debug.Log($"Level '{createdLevel.Name}' uploaded successfully! Id: {createdLevel.Id}");
                    LoadOnlineOwnLevels();
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

        public async void LoadOnlineOwnLevels()
        {
            for (int i = onlineOwnLevelContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(onlineOwnLevelContainer.transform.GetChild(i).gameObject);
            }

            var res = await LevelApi.GetLevelsByUserAsync(GameDataManager.Instance.username);

            for (int i = 0; i < res.Count; i++)
            {
                var levelSummary = res[i];
                var newObj = Instantiate(onlineOwnLevelPrefab, onlineOwnLevelContainer.transform);
                var rowComponent = newObj.GetComponent<LevelBrowserOwnOnlineLevelRow>();
                if (rowComponent != null)
                {
                    rowComponent.Init(levelSummary.Id, levelSummary.Name, levelSummary.Author, this);
                }
            }
            
        }
    }
}