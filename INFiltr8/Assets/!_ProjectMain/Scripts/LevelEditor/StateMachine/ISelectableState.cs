using UnityEngine;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public interface ISelectableState
    {
        public Sprite GetIcon();
        public string GetName();
    }
}