namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public interface ILevelTimeObserver : ITimeObserver
    {
        public void OnChangeMaxTime(double newMaxTime);
        public void OnReachedMaxTime();
        public void OnChangeMultiplier(float newMultiplier);
    }
}