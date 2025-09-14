using System;
using System.Linq;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public class StartSpeedrunButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        public void OnSpeedrunStart()
        {
            LevelLoaderManager.Instance.StartSpeedrun();
            FindFirstObjectByType<SpeedrunTimeTrackerObserver>().ShowTimer();
        }

        private void Start()
        {
            try
            {
                double min = GameDataManager.Instance.gameData.speedrunHistory.Min(x => x.speedrunTime);
                var bestTime = GameDataManager.Instance.gameData.speedrunHistory.Where(x => x.speedrunTime == min)
                    .OrderByDescending(x => x.completionDateTime).FirstOrDefault();
                if (bestTime != null)
                {
                    text.text = "PB: " + StringUtils.ToMinSecMilli(bestTime.speedrunTime);
                }
                else
                {
                    text.text = "No speedrun completed yet.";
                }
            }
            catch (Exception e)
            {
                text.text = "No speedrun completed yet.";
            }
        }
    }
}