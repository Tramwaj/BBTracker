using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace BBTracker.Models
{
    public enum PlayType
    {
        CheckIn,
        CheckOut,
        FieldGoal,
        Assist,
        Rebound,
        Block,
        Steal,
        FreeThrow,
        TurnOver,
        Foul,
        Null
    }
    public class Play
    {
        public Play()
        {
            PlayType = PlayType.Null;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan GameTime { get; set; }        
        public bool TeamB { get; set; }
        public PlayType PlayType { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }

        public int Points { get; set; }
        public bool MadeFG { get; set; }
        public bool BlockedFG { get; set; }
        public bool AssistedFG { get; set; }
        public bool WithFoulFG { get; set; }
        public virtual Player? Assister { get; set; }
        public virtual Player? Blocker { get; set; }
        public virtual Player? Fouler { get; set; }

        public virtual Player? AssistedPlayer { get; set; }

        public bool OffensiveRebound { get; set; }

        public int PointsBlocked { get; set; }
        public virtual Player? BlockedPlayer { get; set; }

        public bool OffensiveFoul { get; set; }
        public bool ShootingFoul { get; set; }
        public virtual Player? FouledPlayer { get; set; }

        public virtual Player? StolenFrom { get; set; }

        public bool ForcedRebound { get; set; }
        public virtual Player? TurnoverCauser { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
