using System;
using System.IO;
using __ProjectMain.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.UI
{
    public class LevelUISelector : MonoBehaviour
    {
        private string _textInput;
        
        [SerializeField] private GameObject levelSelectorContainer;
        [SerializeField] private GameObject levelSelectorPrefab;
        
        public void OnChange(string input) => _textInput = input;
        
        public void CreateNewLevel()
        {
            LevelFileManager.Instance.CreateAndLoadLevel(_textInput);
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var levels = LevelFileManager.Instance.GetLevels();
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
    }
}