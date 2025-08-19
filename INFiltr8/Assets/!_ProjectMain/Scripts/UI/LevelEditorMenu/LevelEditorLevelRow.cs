using __ProjectMain.Scripts.LevelEditor;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI.LevelEditorMenu
{
    public class LevelEditorLevelRow : MonoBehaviour
    {
        public int Index { get; private set; }
        public string FileName { get; private set; }
        public LevelData LevelData {  get; private set; }

        private LevelEditorManager _managerRef;

        public void Init(int index, LevelData levelData, string fileName, LevelEditorManager managerRef)
        {
            Index = index;
            LevelData = levelData;
            FileName = fileName;
            _managerRef = managerRef;
            GetComponentInChildren<TMP_Text>().text = levelData.levelName;
        }

        public void OnPlay() => _managerRef.OnPlay(LevelData);
        public void OnDel() => _managerRef.OnDelete(FileName);
        public void OnEdit() => _managerRef.OnEdit(LevelData);
    }
}