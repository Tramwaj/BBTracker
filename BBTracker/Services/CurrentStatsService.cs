using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.RightsManagement;

namespace BBTracker.Services
{
    public class CurrentStatsService
    {
        public ICollection<PlayerStats> PlayersStatsList { get; private set; }        

        public CurrentStatsService(ICollection<PlayerStats> playerStats)
        {
            PlayersStatsList = playerStats;
        }
          

        public void AddPlay(Play play)
        {
            if (!PlayersStatsList.Any(ps => ps.Player == play.Player))
            {
                PlayersStatsList.Add(new PlayerStats
                {
                    Player = play.Player,
                    Name = play.Player.FirstName + " " + play.Player.Nick[0]
                });

            }
            var _player = PlayersStatsList.First(p => p.Player == play.Player);

            void AddFieldGoal(int numberOfPoints)
            {
                
            }

            switch (play.PlayType)
            {
                case PlayType.Assist:
                    _player.Assists++;
                    break;
                case PlayType.Block:
                    _player.Blocks++;
                    break;
                case PlayType.FieldGoal:
                    if (play.Points == 2)
                    {
                        _player.Attempted2pFG++;
                        if (play.MadeFG)
                        {
                            _player.Made2pFG++;
                            _player.Points += 2;
                        }
                    }
                    if (play.Points == 3)
                    {
                        _player.Attempted3pFG++;
                        if (play.MadeFG)
                        {
                            _player.Made3pFG++;
                            _player.Points += 3;
                        }
                    }                    
                    break;
                case PlayType.Rebound:
                    _player.Rebounds++;
                    if (play.OffensiveRebound)
                    {
                        _player.OffensiveRebounds++;
                    }
                    else
                    {
                        _player.DefensiveRebounds++;
                    }
                    break;
                case PlayType.Steal:
                    _player.Steals++;
                    break;
                case PlayType.TurnOver:
                    _player.Turnovers++;
                    break;
                case PlayType.CheckIn:
                    _player.CheckInTime = DateTime.Now;
                    break;
                case PlayType.CheckOut:
                    _player.PlayingTime += DateTime.Now - _player.CheckInTime;
                    break;
            }
        }
        public void AddPlays(ICollection<Play> plays)
        {
            foreach (var play in plays)
            {
                AddPlay(play);
            }
        }
        public int TotalPoints()
        {
            return PlayersStatsList.Sum(p => p.Points);
        }

    }
    
}

