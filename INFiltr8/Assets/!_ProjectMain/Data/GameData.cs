namespace __ProjectMain.Data
{
    [System.Serializable]
    public class GameData
    {
        public string playerName;
        //just the index of the level that is unlocked!
        public int progress;
        public int playTimeMinutes;
        public bool tutorialDone;
        public bool introDone;
        
        // for deserialization
        public GameData() {}

        public GameData(string playerName)
        {
            this.playerName = playerName;
            this.progress = 0;
            this.playTimeMinutes = 0;
            this.tutorialDone = false;
            this.introDone = false;
        }
    }
}