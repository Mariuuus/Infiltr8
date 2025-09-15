using System;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class IntroCheckerSelection : MonoBehaviour
    {
        private void Start()
        {
            if (!GameDataManager.Instance.gameData.introDone)
            {
                SceneManager.LoadScene("!_ProjectMain/Scenes/Intro");
            }
            GameDataManager.Instance.QuickSave();
        }
    }
}