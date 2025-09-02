using __ProjectMain.Scripts.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.LevelBrowserMenu
{
    public class LevelBrowserOwnOnlineLevelRow : MonoBehaviour

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
            GetComponentInChildren<TMP_Text>().text = "'" + _name;
        }

        public void OnDelete() => _managerRef.OnDeleteOnlineLevel(_id);
    }
}