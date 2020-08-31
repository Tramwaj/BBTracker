using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;

namespace BBTracker.Models
{
    public class PlayerStats : INotifyPropertyChanged
    {
        public Player Player { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int DefensiveRebounds { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int Attempted2pFG { get; set; }
        public int Attempted3pFG { get; set; }
        public int Made2pFG { get; set; }
        public int Made3pFG { get; set; }
        public TimeSpan PlayingTime { get; set; }
        public int Minutes { get => (int)PlayingTime.TotalMinutes; }
        public DateTime CheckInTime { get; set; }

        public double Percentage2pFG { get => Attempted2pFG > 0 ? ((double)Made2pFG / Attempted2pFG) * 100 : 0; }
        public double Percentage3pFG { get => Attempted3pFG > 0 ? ((double)Made3pFG / Attempted3pFG) * 100 : 0; }
        public int MissedFG { get => Attempted2pFG - Made2pFG + Attempted3pFG - Made3pFG; }

        public double Eval { get => Points + Rebounds + Assists + Steals + Blocks - MissedFG - Turnovers; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
