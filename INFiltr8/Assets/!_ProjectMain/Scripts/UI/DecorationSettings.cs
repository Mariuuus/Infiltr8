using System;
using System.Globalization;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
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
        [SerializeField] private TMP_Dropdown variantDropdown;
        [SerializeField] private Slider rotationSlider;
        [SerializeField] private TMP_InputField rotationInput;
        [SerializeField] private TMP_Text variantText;
        
        public void Show(DecorationComponent decorationComponent)
        {
            this.gameObject.SetActive(true);
            this.variantDropdown.gameObject.SetActive(false);
            this.variantText.gameObject.SetActive(false);
            this._decorationComponent = decorationComponent;
            decorationDropdown.options.Clear();
            foreach (var decoration in Enum.GetValues(typeof(Decorations)))
            {
                decorationDropdown.options.Add(new TMP_Dropdown.OptionData(decoration.ToString()));
            }
            decorationDropdown.value = ((Int32) _decorationComponent.decoration);
            rotationSlider.value = _decorationComponent.rotation;
            rotationInput.text = _decorationComponent.rotation.ToString("F1", CultureInfo.CurrentCulture);
            variantDropdown.options.Clear();
            foreach (var variant in Enum.GetValues(typeof(Variations)))
            {
                variantDropdown.options.Add(new TMP_Dropdown.OptionData(variant.ToString()));
            }
            variantDropdown.value = ((Int32) _decorationComponent.variant);

            if (_decorationComponent.decoration == Decorations.Lavalamp || _decorationComponent.decoration == Decorations.TubeLamp)
            {
                this.variantDropdown.gameObject.SetActive(true);
                this.variantText.gameObject.SetActive(true);
            }
        }

        public void OnDecorationSelect(Int32 option)
        {
            Decorations selectedDecoration = (Decorations) option;
            _decorationComponent.decoration = selectedDecoration;
            if ((selectedDecoration == Decorations.Lavalamp || selectedDecoration == Decorations.TubeLamp) && !variantDropdown.IsActive())
            {
                variantDropdown.gameObject.SetActive(true);
                this.variantText.gameObject.SetActive(true);
            }
            else
            {
                variantDropdown.gameObject.SetActive(false);
                this.variantText.gameObject.SetActive(false);
            }
            LevelEditorManager.Instance.UpdateUI();
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void onVariantSelect(Int32 option)
        {
            _decorationComponent.variant = (Variations) option;
            LevelEditorManager.Instance.UpdateUI();
            LevelEditorFileManager.Instance.QuickSave();
        }

        private int previousMappedValue = 0;

        public void OnRotationSliderChange(float value)
        {
            _decorationComponent.rotation = value;
            rotationInput.text = value.ToString("F1", CultureInfo.CurrentCulture);
            if (previousMappedValue != (int) (value / 13))
            {
                previousMappedValue = (int) (value / 13);
                LevelEditorManager.Instance.UpdateUI();           
            }
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnSetRotation(string value)
        {
            float parsed = float.Parse(value);
            _decorationComponent.rotation = Mathf.Clamp(parsed, 0f, 360f);
            rotationSlider.value = Mathf.Clamp(parsed, 0f, 360f);
            // LevelEditorManager.Instance.UpdateUI();
            LevelEditorFileManager.Instance.QuickSave();
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}