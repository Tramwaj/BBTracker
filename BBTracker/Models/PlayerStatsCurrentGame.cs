using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Models
{
    public class PlayerStatsCurrentGame : PlayerStats
    {
        public DateTime CheckInTime { get; set; }
        public TimeSpan CurrentTimeOnTheFloor { get => DateTime.Now - CheckInTime; }
        public TimeSpan CurrentPlayingTime
        {
            get => OnTheFloor ?
PlayingTime + CurrentTimeOnTheFloor : PlayingTime;
        }
        public bool OnTheFloor { get; set; }
        public int Minutes { get => (int)PlayingTime.TotalMinutes; }
    }
}
