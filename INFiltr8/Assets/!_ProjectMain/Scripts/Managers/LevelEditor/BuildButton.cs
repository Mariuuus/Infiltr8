using __ProjectMain.Scripts.Managers.LevelEditor.InteractableFields;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class BuildButton : MonoBehaviour
    {
        private int _index;
        private PlaceableLevelComponent _component;
        public Image image;
        public void OnClick()
        {
            LevelUIManager.Instance.currentSelectedBuildComponent = this._index;
        }
        
        public void Init(int index, PlaceableLevelComponent component)
        {
            this._component = component;
            this._index = index;

            image.sprite = _component.BuildMenuIcon;
        }
    }
}