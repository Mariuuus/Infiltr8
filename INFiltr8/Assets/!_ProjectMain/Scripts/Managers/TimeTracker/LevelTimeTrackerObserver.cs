using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public class LevelTimeTrackerObserver : MonoBehaviour, ILevelTimeObserver
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color pausedColor = Color.yellow;
        [SerializeField] private Color speedUp = new(1f, 0.55f, 0.02f);
        [SerializeField] private Color closeToMaxColor = Color.red;
        [SerializeField] private double closeToMaxColorDistance = 5f;

        public void Init(bool hasTime)
        {
            Debug.Log("LevelTimeTrackerObserver.Init()" + hasTime);
            timeText.gameObject.SetActive(hasTime);
            if(hasTime) LevelTimeTracker.Instance.Subscribe(this);
        }

        public void OnPauseChanged(bool newValue)
        {
            timeText.color = newValue ? pausedColor : defaultColor;
        }

        public void OnTimeChange(double time)
        {
            timeText.text = $"{time:F2}/{LevelTimeTracker.Instance.MaxTime:F2}\n{(!Mathf.Approximately(LevelTimeTracker.Instance.Multiplier, 1) ? "3.33x " : "")}";
            timeText.color = LevelTimeTracker.Instance.MaxTime-time < closeToMaxColorDistance ? closeToMaxColor : !Mathf.Approximately(LevelTimeTracker.Instance.Multiplier, 1) ? speedUp : defaultColor;
        }

        public void OnSecondsChange(int seconds) {}

        public void OnChangeMaxTime(double newMaxTime)
        {
            OnTimeChange(LevelTimeTracker.Instance.CurrentTime);
        }

        public void OnReachedMaxTime()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnChangeMultiplier(float newMultiplier)
        {
            OnTimeChange(LevelTimeTracker.Instance.CurrentTime);
        }
    }
}