using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBTracker.Services
{
    public class PlayersStatsService
    {
        public ICollection<PlayerStats> TeamAPlayers { get; set; }
        public ICollection<PlayerStats> TeamBPlayers { get; set; }
        public ICollection<PlayerStats> AllPlayers { get=>TeamAPlayers.Concat(TeamBPlayers).ToList();}

        public PlayersStatsService(IEnumerable<Play> plays)
        {
            TeamAPlayers = new List<PlayerStats>();
            TeamBPlayers = new List<PlayerStats>();

            CurrentStatsService statCounterA = new CurrentStatsService(TeamAPlayers);
            CurrentStatsService statCounterB = new CurrentStatsService(TeamBPlayers);

            foreach (Play play in plays.Where(p=>p.TeamB==false))
            {
                statCounterA.AddPlay(play);
            }
            foreach (Play play in plays.Where(p => p.TeamB == true))
            {
                statCounterB.AddPlay(play);
            }
        }

    }
}
