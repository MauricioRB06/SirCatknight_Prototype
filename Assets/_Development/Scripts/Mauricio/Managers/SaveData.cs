using System;

namespace _Development.Scripts.Mauricio.Managers
{
    [Serializable]
    public class SaveData
    {
        public PlayerData MyPlayerData { get; set; }

        public SaveData()
        {
            
        }
    }
    
    [Serializable]
    public class PlayerData
    {
        public int CurrentLevel { get; set; }

        public PlayerData(int level)
        {
            CurrentLevel = level;
        }
    }
}
