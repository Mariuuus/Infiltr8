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
            try
            {
                LevelFileManager.Instance.levelData = LevelFileManager.Instance.LoadFile(_textInput);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Debug.Log(fileNotFoundException.Message);
                LevelFileManager.Instance.levelData = LevelFileManager.Instance.CreateLevel(_textInput);
                LevelFileManager.Instance.QuickSave();
            }
            finally
            {
                SceneManager.LoadScene("LevelEditor");
            }
        }
    }
}