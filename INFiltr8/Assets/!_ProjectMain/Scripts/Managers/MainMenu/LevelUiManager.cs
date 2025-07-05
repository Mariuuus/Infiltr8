using __ProjectMain.Scripts.UI.MainMenuElements;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers.MainMenu
{
    public class LevelUiManager : MonoBehaviour
    {
        public GameObject levelContainer;
        public GameObject prefabButton;
        public bool cheat;
        
        public void Start()
        {
            var level = LevelDataUtils.LoadLevels();

            Debug.Log($"Loaded {level.Length} levels");

            for (int i = 0; i < level.Length; i++)
            {
                var levelData = level[i];
                var newObject = Instantiate(prefabButton, levelContainer.transform, false);
                //TODO: add available state in a gamesave folder
                bool available = cheat;
                newObject.GetComponent<LevelStartButton>().Init(levelData, i+1, available);
            }
        }
        
    }
}