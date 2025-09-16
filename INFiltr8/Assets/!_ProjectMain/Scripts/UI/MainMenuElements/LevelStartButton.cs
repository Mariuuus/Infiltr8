using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using LevelType = __ProjectMain.Scripts.LevelEditor.LevelType;

namespace __ProjectMain.Scripts.UI.MainMenuElements
{
    public class LevelStartButton : MonoBehaviour
    {
        public TMP_Text buttonText;
        public Image buttonImage;
        public LevelData levelData;
        public int levelNumber;
        public int levelName;
        
        public Image iconImage;
        public Sprite timeSprite;
        public Sprite goalSprite;
        public Sprite tutorialSprite;
        
        public void Init(LevelData pLevelData, int pLevelNumber, bool available, bool isNextLevel, bool isGoal)
        {
            levelData = pLevelData;
            levelNumber = pLevelNumber;
            buttonText.text = "Level " + levelNumber;
            if(isGoal)
            {
                iconImage.sprite = goalSprite;
                return;
            }
            if (levelData.isPartOfTutorial)
            {
                iconImage.sprite = tutorialSprite;
                buttonImage.color = available
                    ? new Color32(138, 248, 255, 255)
                    : new Color32(138, 248, 255,90); 
            }
            else
            {
                switch (levelData.levelType)
                {
                    case LevelType.GetData:
                    case LevelType.Silent:
                        iconImage.sprite = goalSprite;
                        iconImage.color = new Color32(0, 0, 0, 0);
                        buttonImage.color = available
                            ? new Color32(255, 243, 113, 255)
                            : new Color32(255, 243, 113, 90); 
                        break;
                    default:
                        iconImage.sprite = timeSprite;
                        buttonImage.color = available
                            ? new Color32(255, 134, 101, 255)
                            : new Color32(255, 134, 101,  90); 
                        break;
                }
            }

            if(!isNextLevel) GetComponent<Outline>().enabled = false;
        }

        public void OnClick()
        {
            FindFirstObjectByType<ComputerLevel>().OnPreview(
                levelData, levelNumber);
        }
    }
}