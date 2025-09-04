using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class DecorationSettings: MonoBehaviour
    {
        private DecorationComponent _decorationComponent;
        [SerializeField] private TMP_Dropdown decorationDropdown;
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
        }

        public void onDecorationSelect(Int32 option)
        {
            _decorationComponent.decoration = (Decorations) option;
            LevelEditorFileManager.Instance.QuickSave();
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}