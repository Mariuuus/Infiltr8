using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class SelectableLevel : MonoBehaviour
    {
        public int Index { get; set; }
        public LevelData LevelData { get; set; }
        
        [SerializeField] private TMP_Text levelNameText;

        public void OnEdit()
        {
            LevelFileManager.Instance.EditLevel(Index);
        }
        
        public void OnPlay()
        {
            LevelFileManager.Instance.PlayLevel(Index);
        }

        public void UpdateUI()
        {
            levelNameText.text = LevelData.levelName;
        }
    }
}