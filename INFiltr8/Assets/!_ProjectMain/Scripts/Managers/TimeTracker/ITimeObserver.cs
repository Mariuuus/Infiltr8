namespace __ProjectMain.Scripts.Managers.TimeTracker
{
    public interface ITimeObserver
    {
        public void OnPauseChanged(bool newValue);
        public void OnTimeChange(double time);
        public void OnSecondsChange(int seconds);
    }
}