using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.Objects;

namespace __ProjectMain.Data
{  
    [Serializable]
    public class CollectDistro
    {
        public CollectDistro(DistroType distroType,  bool collect)
        {
            this.distroType =  distroType;
            this.collect = collect;
        }
        public DistroType distroType;
        public bool collect;
    }

    [Serializable]
    public class SpeedrunEntry
    {
        public double speedrunTime;
        public DateTime completionDateTime;

        public SpeedrunEntry(double speedrunTime)
        {
            this.speedrunTime = speedrunTime;
            completionDateTime = DateTime.Now;
        }
    }
    
    [Serializable]
    public class GameData
    {
        public string playerName;
        //just the index of the level that is unlocked!
        public int progress;
        public int playTimeMinutes;
        public List<SpeedrunEntry> speedrunHistory;
        public bool tutorialDone;
        public bool outroDone;
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public bool introDone;
        public List<CollectDistro> collectedDistros = new List<CollectDistro>();
        
        // for deserialization
        public GameData() {}

        public GameData(string playerName)
        {
            this.playerName = playerName;
            this.progress = 0;
            this.playTimeMinutes = 0;
            this.masterVolume = 0.75f;
            this.musicVolume = 0.2f;
            this.sfxVolume = 1;
            this.tutorialDone = false;
            this.introDone = false;
            this.outroDone = true;
            this.speedrunHistory = new List<SpeedrunEntry>();
            foreach (var obj in Enum.GetValues(typeof(DistroType)))
            {
                var distro = (DistroType)obj;
                collectedDistros.Add(new CollectDistro(distro, false));
            }
        }

        public void AddSpeedrunEntry(SpeedrunEntry speedrunEntry)
        {
            if (speedrunHistory == null) speedrunHistory = new List<SpeedrunEntry>();
            speedrunHistory.Add(speedrunEntry);
        }
    }
}