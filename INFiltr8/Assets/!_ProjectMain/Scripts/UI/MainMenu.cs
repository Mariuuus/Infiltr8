using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.UI
{
    public class MainMenu : MonoBehaviour

    {
    public void PlayClick() => SceneManager.LoadScene("TestingGround");

    public void LevelEditorButtonClick() => SceneManager.LoadScene("LevelEditorMenu");

    public void SettingsButtonClick() =>  SceneManager.LoadScene("SettingsMenu");

    public void ExitButtonClick() => Application.Quit();
    }
}