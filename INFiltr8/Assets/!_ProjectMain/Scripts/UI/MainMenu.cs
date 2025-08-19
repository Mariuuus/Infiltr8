using __ProjectMain.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

//DEPRECATED
namespace __ProjectMain.Scripts.UI
{
    public class MainMenu : MonoBehaviour

    {
    public void PlayClick()  {
        Debug.Log("PlayClick");
        GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine.IngameState);
    }

    public void LevelEditorButtonClick()
    {
        Debug.Log("LevelEditorButtonClick");
        GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine.LevelEditorMenuState);
    }


    public void SettingsButtonClick() =>  SceneManager.LoadScene("SettingsMenu");

    public void ExitButtonClick() => Application.Quit();
    }
}