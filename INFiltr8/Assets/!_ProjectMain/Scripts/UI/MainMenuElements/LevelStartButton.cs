using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.MainMenuElements
{
    public class LevelStartButton : MonoBehaviour
    {
        public TMP_Text buttonText;
        public Image buttonImage;
        public LevelData levelData;
        public int levelNumber;
        public int levelName;
        
        public void Init(LevelData pLevelData, int pLevelNumber, bool available)
        {
            levelData = pLevelData;
            levelNumber = pLevelNumber;
            buttonText.text = "Level " + levelNumber;
            buttonImage.color = available ? new Color(255, 243, 113, 255) : new Color(255, 243, 113, 64);
        }

        public void OnClick()
        {
            LevelLoaderManager.Instance?.LoadLevel(levelData);
        }
    }
}