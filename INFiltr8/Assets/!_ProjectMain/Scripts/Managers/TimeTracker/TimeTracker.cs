using System;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public abstract class TimeTracker<T> : MonoBehaviour 
        where T : ITimeObserver    
    {
        public double CurrentTime { get; private set; }
        public bool Paused { get; private set; } = true;
        
        private readonly List<T> _listeners = new List<T>();

        public void Subscribe(T listener)
        {
            _listeners.Add(listener);
            listener.OnTimeChange(CurrentTime);
        }

        public void UnSubscribe(T listener)
        {
            _listeners.Remove(listener);
        }

        public void ManipulateTime(double byTime)
        {
            CurrentTime += byTime;
        }

        protected void ForAllListeners(Action<T> action)
        {
            foreach (var listener in _listeners)
            {
                action(listener);
            }
        }

        private void SendUpdateTime() => ForAllListeners((observer) => observer.OnTimeChange(CurrentTime));
        private void SendUpdateSeconds() => ForAllListeners((observer) => observer.OnSecondsChange((int)CurrentTime));
        
        private void SendUpdateOnPausedChange() => ForAllListeners((observer) => observer.OnPauseChanged(Paused));

        private void Start()
        {
            Paused = false;
        }

        private void Update()
        {
            double previousTime = CurrentTime;
            if(!Paused) CurrentTime += Time.deltaTime;
            if((int)(previousTime) != (int)CurrentTime) SendUpdateSeconds();
            SendUpdateTime();
            OnUpdate(previousTime, CurrentTime);
        }
        
        protected abstract void OnUpdate(double previousTime, double newTime);

        public void PauseTime() {
            Paused = true;
            SendUpdateOnPausedChange();
        }
        public void ResumeTime()
        {
            Paused = false;
            SendUpdateOnPausedChange();
        }
        
        public void ResetTime() => CurrentTime = 0;
    }
}