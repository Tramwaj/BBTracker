using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BBTracker.Models
{
    public class Game : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
        public virtual ICollection<Play> Plays { get; set; } = new List<Play>();
        public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
