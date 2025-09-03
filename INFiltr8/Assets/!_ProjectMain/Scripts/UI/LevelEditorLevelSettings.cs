using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class LevelEditorLevelSettings : MonoBehaviour
    {
        [SerializeField] private TMP_InputField maxTimeInputField;
        [SerializeField] private TMP_Dropdown levelTypeDropdown;
        private void Start()
        {
            levelTypeDropdown.options.Clear();
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var val in Enum.GetValues(typeof(LevelType)))
            {
                options.Add(new TMP_Dropdown.OptionData(val.ToString()));
            }
            levelTypeDropdown.options.AddRange(options);
        }
        
        public void Show(LevelData levelData)
        {
            this.gameObject.SetActive(true);
            levelTypeDropdown.value = (Int32)levelData.levelType;
            levelTypeDropdown.RefreshShownValue();
            maxTimeInputField.text = levelData.availableTime.ToString();
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void OnOptionSelect(Int32 option)
        {
            LevelEditorFileManager.Instance.levelData.levelType = (LevelType)option;
            LevelEditorFileManager.Instance.QuickSave();
        }
        
        public void OnAmountChanged(String amount)
        {
            LevelEditorFileManager.Instance.levelData.availableTime = float.Parse(amount);
            LevelEditorFileManager.Instance.QuickSave();
        }
    }
}