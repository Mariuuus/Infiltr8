using UnityEngine;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public class LevelTimeTracker : TimeTracker<ILevelTimeObserver>
    {
        public double MaxTime { get; private set;  }
        public static LevelTimeTracker Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        
        private void SendUpdateOnMaxReached() => ForAllListeners((observer) => observer.OnReachedMaxTime());
        private void SendUpdateOnMaxChanged() => ForAllListeners((observer) => observer.OnChangeMaxTime(MaxTime));
        private void SendUpdateOnMultiplierChanged() => ForAllListeners((observer) => observer.OnChangeMultiplier(Multiplier));


        public void SetMaxTime(double newMaxTime)
        {
            MaxTime = newMaxTime;
            SendUpdateOnMaxChanged();
        }
        
        public void SetMultiplier(float multiplier)
        {
            Multiplier = multiplier;
            SendUpdateOnMultiplierChanged();
        }

        protected override void OnUpdate(double previousTime, double newTime)
        {
            if (newTime > MaxTime)
            {
                SendUpdateOnMaxReached();
            }
        }
    }
}