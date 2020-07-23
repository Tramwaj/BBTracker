using System;
using System.Collections.Generic;
using System.Text;

namespace NBAcsvPlayByPlayReader
{
    public class PlayFromCSV
    {
        public string URL { get; set; }
        public string GameType { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string WinningTeam { get; set; }
        public int Quarter { get; set; }
        public int SecLeft { get; set; }
        public string AwayTeam { get; set; }
        public string AwayPlay { get; set; }
        public string AwayScore { get; set; }
        public string HomeTeam { get; set; }
        public string HomePlay { get; set; }
        public string HomeScore { get; set; }
        public string Shooter { get; set; }
        public string ShotType { get; set; }
        public string ShotOutcome { get; set; }
        public int ShotDist { get; set; }
        public string Assister { get; set; }
        public string Blocker { get; set; }
        public string FoulType { get; set; }
        public string Fouler { get; set; }
        public string Fouled { get; set; }
        public string Rebounder { get; set; }
        public string ReboundType { get; set; }
        public string ViolationPlayer { get; set; }
        public string ViolationType { get; set; }
        public string TimeoutTeam { get; set; }
        public string FreeThrowShooter { get; set; }
        public string FreeThrowOutcome { get; set; }
        public string FreeThrowNum { get; set; }
        public string EnterGame { get; set; }
        public string LeaveGame { get; set; }
        public string TurnoverPlayer { get; set; }
        public string TurnoverType { get; set; }
        public string TurnoverCause { get; set; }
        public string TurnoverCauser { get; set; }
        public string JumpballAwayPlayer { get; set; }
        public string JumpballHomePlayer { get; set; }
        public string JumpballPoss { get; set; }

    }
}
