using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Components;

namespace __ProjectMain.Scripts.LevelEditor.StateMachine
{
    public interface IConnectedComponent
    {
        public void OnRemove();
        public List<LevelComponent> GetAllLevelComponents();
    }
}