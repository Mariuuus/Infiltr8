using System.Collections.Generic;

namespace __ProjectMain.Scripts.LevelEditor.Components
{
    public interface IConnectedComponent
    {
        public void OnRemove();
        public List<LevelComponent> GetAllLevelComponents();
    }
}