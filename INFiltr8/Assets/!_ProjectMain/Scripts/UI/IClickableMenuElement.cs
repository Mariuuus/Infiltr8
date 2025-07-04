using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public interface IClickableMenuElement
    {
        public void OnHoverStart();
        public void OnHoverEnd();
        public void OnClick();
        public void OnUnclick();
    }
}