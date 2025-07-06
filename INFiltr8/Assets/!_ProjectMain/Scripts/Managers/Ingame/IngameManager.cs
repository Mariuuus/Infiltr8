using System;
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

        public void Pause() =>  Paused = true;
        public void Resume() =>  Paused = false;

        public void Quit() => GameDataManager.Instance.SwitchToOverview();
    }
}