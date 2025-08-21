using __ProjectMain.Scripts.LevelEditor;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI.LevelBrowserMenu
{
    public class LevelBrowserLevelRow : MonoBehaviour

    {
        private string _id;
        private string _name;
        private string _author;

        private LevelBrowserManager _managerRef;

        public void Init(string id, string pName, string pAuthor, LevelBrowserManager managerRef)
        {
            _id = id;
            _name = pName;
            _author =  pAuthor;
            _managerRef = managerRef;
            GetComponentInChildren<TMP_Text>().text = "'" + _name + "', from: " + _author;
        }

        public void OnSelect() => _managerRef.OnSelectLevel(_id);
    }
}