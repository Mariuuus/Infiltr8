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
    public class GameData
    {
        public string playerName;
        //just the index of the level that is unlocked!
        public int progress;
        public int playTimeMinutes;
        public bool tutorialDone;
        public bool introDone;
        public List<CollectDistro> collectedDistros = new List<CollectDistro>();
        
        // for deserialization
        public GameData() {}

        public GameData(string playerName)
        {
            this.playerName = playerName;
            this.progress = 0;
            this.playTimeMinutes = 0;
            this.tutorialDone = false;
            this.introDone = false;
            foreach (var obj in Enum.GetValues(typeof(DistroType)))
            {
                var distro = (DistroType)obj;
                collectedDistros.Add(new CollectDistro(distro, false));
            }
        }
    }
}