using __ProjectMain.Scripts.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.LevelBrowserMenu
{
    public class LevelBrowserLocalLevelRow : MonoBehaviour

    {
        private string _name;
        private LevelData _levelData;
        private LevelBrowserManager _managerRef;

        public void Init(string pName, LevelData lvlData, bool selected, LevelBrowserManager managerRef)
        {
            _name = pName;
            _levelData = lvlData;
            _managerRef = managerRef;
            GetComponentInChildren<TMP_Text>().text = _name;
            GetComponentInChildren<Button>().interactable = !selected;
        }

        public void OnSelect() => _managerRef.OnSelectLocalLevel(_levelData);
    }
}