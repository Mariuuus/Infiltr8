using System;
using System.Drawing;
using System.IO;
using __ProjectMain.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.UI
{
    public class LevelUISelector : MonoBehaviour
    {
        private string _textInput;
        private int _size;
        
        [SerializeField] private GameObject levelSelectorContainer;
        [SerializeField] private GameObject levelSelectorPrefab;
        
        [SerializeField] private TMP_Text sizeRepresentation;
        
        
        public void OnNameChange(String input) => _textInput = input;


        public void OnSliderChange(Single element)
        {
            sizeRepresentation.text = "Size: " + (int)element; 
            _size = (int)element;
        }
        
        
        public void CreateNewLevel()
        {
            LevelFileManager.Instance.CreateAndLoadLevel(_textInput, _size);
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