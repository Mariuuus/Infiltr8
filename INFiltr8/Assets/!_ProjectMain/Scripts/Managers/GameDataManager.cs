using System;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            GameData gd = GameDataUtils.LoadData();

            if (!gd.introDone)
            {
                Debug.Log("introDone");
                CameraManager.Instance.GameStartSequence();
            }
            
            Debug.developerConsoleVisible = true;
        }

        public void SwitchToOverview()
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }
}