using System.IO;
using __ProjectMain.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.UI
{
    public class LevelUISelector : MonoBehaviour
    {
        private string _textInput;
        
        public void OnChange(string input) => _textInput = input;
        
        public void CreateNewLevel()
        {
            LevelFileManager.Instance.CreateAndLoadLevel(_textInput);
        }
    }
}