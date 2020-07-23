using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BBTracker.Services
{
    public class CurrentStatsService
    {
        public ObservableCollection<PlayerStats> PlayersStatsList { get; set; }

        public CurrentStatsService(ObservableCollection<PlayerStats> playerStats)
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
                        _player.DefensiveRebound++;
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

    }
    public class PlayerStats : INotifyPropertyChanged
    {
        public Player Player { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int DefensiveRebound { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int Attempted2pFG { get; set; }
        public int Attempted3pFG { get; set; }
        public int Made2pFG { get; set; }
        public int Made3pFG { get; set; }
        public double Percentage2pFG { get => Attempted2pFG > 0 ? (Made2pFG / Attempted2pFG) * 100 : 0; private set { } }
        public double Percentage3pFG { get => Attempted3pFG > 0 ? (Made3pFG / Attempted3pFG) * 100 : 0; private set { } }
        public TimeSpan PlayingTime { get ; set; }
        public int Minutes { get => (int)PlayingTime.TotalMinutes; }
        public DateTime CheckInTime { get; set; }
                
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

