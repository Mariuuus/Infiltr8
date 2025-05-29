using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.UI;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelFileManager : MonoBehaviour
    {
        public static LevelFileManager Instance { get; private set; }
        public LevelData LevelToLoad { get; private set; }
        
        [SerializeField] private GameObject levelSelectorContainer;
        [SerializeField] private GameObject levelSelectorPrefab;

        public void Awake()
        {
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();
        }

        private void Init()
        {
            var levels = GetLevels();
            for (int i = levelSelectorContainer.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(levelSelectorContainer.transform.GetChild(i).gameObject);
            }
            Debug.Log(levels);
            Debug.Log(levels.Count);
            for (int i = 0; i < levels.Count; i++)
            {
                GameObject newElement = Instantiate(levelSelectorPrefab, levelSelectorContainer.transform);
                var uisel = newElement.GetComponent<SelectableLevel>();
                uisel.LevelData = levels[i];
                uisel.Index = i;
                uisel.UpdateUI();
            }
        }

        public void SelectLevel(int index)
        {
            LevelToLoad = GetLevels()[index];
            SceneManager.LoadScene("LevelEditor");
        }

        public List<LevelData> GetLevels() => LevelDataUtils.GetAvailableLevels();
        
        public void CreateAndLoadLevel(string levelName) {
            try
            {
                LevelToLoad = LevelDataUtils.LoadFile(levelName);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                LevelDataUtils.SaveFile(new LevelData(levelName), false);
                LevelToLoad = LevelDataUtils.LoadFile(levelName); 
            }
            finally
            {
                SceneManager.LoadScene("LevelEditor");
            }
        }
        
    }
}