using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.DataClasses
{
    public class GameState
    {
        public int CurrentScore { get; set; }
        public int CurrentTime { get; set; }
        public bool IsPaused { get; set; }
        public bool IsGameEnded { get; set; }
        public string CurrentSave { get; set; }
        public double CurrentDifficultyMultiplier { get; set; }
        public int CurrentCharacter { get; set; }

        public GameState()
        {
            CurrentScore = 0;
            CurrentTime = 0;
            IsPaused = false;
            IsGameEnded = false;
            CurrentSave = string.Empty;
            CurrentDifficultyMultiplier = -1.0;
            CurrentCharacter = -1;
        }
    }
}
