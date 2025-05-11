using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class BuildButton : MonoBehaviour
    {
        private int _index;
        private PlaceableComponentWithCallback _component;
        public Image image;
        public void OnClick()
        {
            LevelUIManager.Instance.currentSelectedBuildComponent = this._index;
        }
        
        public void Init(int index, PlaceableComponentWithCallback component)
        {
            this._component = component;
            this._index = index;

            image.sprite = _component.component.BuildMenuIcon;
        }
    }
}