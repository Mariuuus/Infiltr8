using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace __ProjectMain.Scripts.States.Components
{   
    [System.Serializable]
    public class LevelComponent
    {
        //might be no need, this class is just for structure.

        public LevelComponent()
        {
            if(LevelEditorUtils.IsComponentOnField(LevelFileManager.Instance.levelData, this)) throw new InvalidLevelEditorException("This component is not inside the field!");
        }
    }
}