using System;
using System.Globalization;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class DecorationSettings: MonoBehaviour
    {
        private DecorationComponent _decorationComponent;
        [SerializeField] private TMP_Dropdown decorationDropdown;
        [SerializeField] private Slider rotationSlider;
        [SerializeField] private TMP_InputField rotationInput;
        
        public void Show(DecorationComponent decorationComponent)
        {
            this.gameObject.SetActive(true);
            this._decorationComponent = decorationComponent;
            decorationDropdown.options.Clear();
            foreach (var decoration in Enum.GetValues(typeof(Decorations)))
            {
                decorationDropdown.options.Add(new TMP_Dropdown.OptionData(decoration.ToString()));
            }
            decorationDropdown.value = ((Int32) _decorationComponent.decoration);
            rotationSlider.value = _decorationComponent.rotation;
            rotationInput.text = _decorationComponent.rotation.ToString("F1", CultureInfo.CurrentCulture);
        }

        public void OnDecorationSelect(Int32 option)
        {
            _decorationComponent.decoration = (Decorations) option;
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnRotationSliderChange(float value)
        {
            _decorationComponent.rotation = value;
            rotationInput.text = value.ToString("F1", CultureInfo.CurrentCulture);
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnSetRotation(string value)
        {
            float parsed = float.Parse(value);
            _decorationComponent.rotation = Mathf.Clamp(parsed, 0f, 360f);
            rotationSlider.value = Mathf.Clamp(parsed, 0f, 360f);
            LevelEditorFileManager.Instance.QuickSave();
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}