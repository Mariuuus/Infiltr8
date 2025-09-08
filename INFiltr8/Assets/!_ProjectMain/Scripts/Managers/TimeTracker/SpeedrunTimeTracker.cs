using __ProjectMain.Scripts.Managers.MainMenu;
using Unity.VisualScripting;

namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public class SpeedrunTimeTracker : TimeTracker<ITimeObserver>
    {

        public static SpeedrunTimeTracker Instance { get; private set; }

        protected override void OnUpdate(double previousTime, double newTime)
        {
            // looooool
        }
        
        private void Start()
        {
            // ResumeTime();
        }
        
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
    }
}