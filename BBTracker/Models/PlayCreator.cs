//using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using System.Linq;

namespace BBTracker.Models
{
    public class PlayCreator
    {
        private List<Play> _plays;
        private Play _play;
        private readonly Game _game;
        public bool HasPlay => _plays.Any();

        public PlayCreator(Game game)
        {
            _game = game;
            _play = new Play();
            _plays = new List<Play>();
        }


        public void NewPlay(Player player, bool teamB)
        {
            _play.Player = player;
            //if (this.HasPlay) ConsecutivePlay(player, teamB);

            _play.TeamB = teamB;
            _play.Date = DateTime.Now.Date;
            _play.GameTime = DateTime.Now - _game.Start;
            _play.Game = _game;
        }

        public void ConsecutivePlay(Player player)
        {
            if (_plays.Count == 0)
            {
                throw new InvalidOperationException();
            }
            _play.Player = player;
            UpdateAdditionalFields(player);
            _plays.Add(_play);
        }

        public void ConsecutivePlay(Player player, bool TeamB)
        {
            if (_play.PlayType != PlayType.Rebound)
            {
                ConsecutivePlay(player);
            }
            else
            {
                _play.Player = player;
                _play.TeamB = TeamB;
                _play.OffensiveRebound = _play.TeamB == _plays.Last().TeamB;
                _plays.Add(_play);
            }
        }

        private void UpdateAdditionalFields(Player player)
        {
            switch (_play.PlayType)
            {
                case PlayType.Assist:
                    _plays.Last().Assister = player;
                    _play.AssistedPlayer = _plays.Last().Player;
                    _play.TeamB = _plays.Last().TeamB;
                    break;
                case PlayType.Block:
                    _plays.Last().Blocker = player;
                    _play.BlockedPlayer = _plays.Last().Player;
                    _play.TeamB = !_plays.Last().TeamB;
                    break;
                case PlayType.Steal:
                    _plays.Last().TurnoverCauser = player;
                    _play.StolenFrom = _plays.Last().Player;
                    _play.TeamB = !_plays.Last().TeamB;
                    break;
                case PlayType.Foul:
                    _plays.Last().Fouler = player;
                    _play.FouledPlayer = _plays.Last().Player;
                    _play.TeamB = !_plays.Last().TeamB;
                    break;
                case PlayType.Rebound:
                    _play.OffensiveRebound = _play.TeamB == _plays.Last().TeamB;
                    throw new InvalidOperationException();
                    break;
            }
        }



        public void ChoosePlayType(PlayType playType)
        {
            _play.PlayType = playType;
            switch (playType)
            {
                case PlayType.CheckOut:
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.CheckIn;
                    break;
                case PlayType.TurnOver:
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Steal;
                    break;
                case PlayType.CheckIn:
                    _plays.Add(_play);
                    break;
            }
        }
        private Play CopyPlay()
        {
            return new Play
            {
                Date = _play.Date,
                Game = _play.Game,
                GameTime = _play.GameTime,
                Points = _play.Points
            };
        }
        private void Cancel()
        {
            _play = new Play();
            _plays = new List<Play>();
        }

        public void Clear()
        {
            Cancel();
        }


        /// <summary>
        /// takes three values: "make","miss","block"
        /// </summary>
        /// <param name="result"></param>
        public void FGResult(string result)
        {
            if (_play.PlayType != PlayType.FieldGoal)
                throw new InvalidOperationException("Play is not a Field goal");
            switch (result)
            {
                case "Make":
                    _play.MadeFG = true;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Assist;
                    break;
                case "Miss":
                    _play.MadeFG = false;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Rebound;
                    break;
                case "Block":
                    _play.MadeFG = false;
                    _play.BlockedFG = true;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Block;
                    break;
            }
        }

        public void SelectPoints(string points)
        {
            _play.Points = Int32.Parse(points);
        }

        // public void ChoosePlaytype(PlayType playType)
        public ICollection<Play> GetPlays() //Flush()??
        {
            return _plays;
        }
    }
}
