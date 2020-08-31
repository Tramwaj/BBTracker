using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace BBTracker.Models
{
    [Serializable]
    public class Game : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
       
        public virtual ICollection<Play> Plays { get; set; } = new List<Play>();
      
        public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();
        
        public TimeSpan Duration { get => End - Start; }

        public Game()
        {
            Start = DateTime.Now;
            ScoreA = 0;
            ScoreB = 0;
        }
        
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
