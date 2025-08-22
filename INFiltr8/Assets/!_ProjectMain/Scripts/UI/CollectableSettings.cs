using __ProjectMain.Scripts.LevelEditor.Components;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class CollectableSettings : MonoBehaviour
    {
        private CollectableComponent _currentCollectable;
        public void ShowAndLoad(CollectableComponent  collectable)
        {
            _currentCollectable = collectable;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}