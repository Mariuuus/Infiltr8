using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Objects;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class CollectableSettings : MonoBehaviour
    {
        private CollectableComponent _currentCollectable;
        [SerializeField] private TMP_Dropdown dropdown;

        public void ShowAndLoad(CollectableComponent collectable)
        {
            _currentCollectable = collectable;
            gameObject.SetActive(true);
            
            dropdown.options.Clear();
            foreach (var character in Enum.GetValues(typeof(DistroType)))
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(character.ToString()));
            }

            dropdown.value = ((Int32) _currentCollectable.type);
        }

        public void OnSelect(Int32 newType)
        {
            _currentCollectable.type = (DistroType) newType;
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}