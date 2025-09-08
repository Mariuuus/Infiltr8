using System;
using __ProjectMain.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public class SpeedrunTimeTrackerObserver : MonoBehaviour, ITimeObserver
    {

        [SerializeField] private TMP_Text text;
        
        private void Start()
        {
            SpeedrunTimeTracker.Instance.Subscribe(this);
        }

        public void OnPauseChanged(bool newValue)
        {
            text.color = newValue ? new Color(0.76f, 0.76f, 0.76f) : Color.white;
        }

        public void OnTimeChange(double time)
        {
            string finalTime = StringUtils.ToMinSecMilli(time);
            text.text = $"{finalTime}";
        }

        public void OnSecondsChange(int seconds)
        {
            
        }

        public void ShowTimer()
        {
            text.gameObject.SetActive(true);
        }

        public void HideTimer()
        {
            text.gameObject.SetActive(false);
        }
    }
}