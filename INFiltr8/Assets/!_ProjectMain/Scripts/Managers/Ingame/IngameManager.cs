using System;
using __ProjectMain.Scripts.Managers.Level;
using __ProjectMain.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.Ingame
{
    public class IngameManager : MonoBehaviour
    {
        public static IngameManager Instance { get; private set; }
        
        public bool Paused { get; private set; }

        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Pause() {
            Paused = true;
            LevelLoaderManager.Instance.playerObject.GetComponent<PlayerController>().Freeze();
        }

        public void Resume()
        {
            Paused = false;
            LevelLoaderManager.Instance.playerObject.GetComponent<PlayerController>().UnFreeze();
        }

        public void Quit() => GameDataManager.Instance.SwitchToOverview();
    }
}