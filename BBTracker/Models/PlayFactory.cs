//using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using System.Linq;

namespace BBTracker.Models
{
    public class PlayFactory
    {
        private List<Play> _plays;
        private Play _play;
        private readonly Game _game;
        public bool HasPlay => _plays.Any();

        public PlayFactory(Game game)
        {
            _game = game;
            _play = new Play();
            _plays = new List<Play>();
        }


        public void SelectPlayer(Player player, bool teamB)
        {
            _play.Player = player;
            if (this.HasPlay) SelectSecondPlayer(player, teamB);

            _play.TeamB = teamB;
            _play.Date = DateTime.Now.Date;
            _play.GameTime = DateTime.Now - _game.Start;
            _play.Game = _game;
        }

        private void SelectSecondPlayer(Player player, bool teamB)
        {
            _play.Player = player;
            _play.TeamB = teamB;
            _plays.Add(_play);
        }

        public void Cancel()
        {
            _play = new Play();
            _plays = new List<Play>();
        }

        public void Clear()
        {
            Cancel();
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
                case "make":
                    _play.MadeFG = true;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Assist;
                    break;
                case "miss":
                    _play.MadeFG = false;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Rebound;
                    break;
                case "block":
                    _play.MadeFG = false;
                    _play.BlockedFG = true;
                    _plays.Add(_play);
                    _play = CopyPlay();
                    _play.PlayType = PlayType.Block;
                    break;
            }
        }

        public void SelectPoints(int points)
        {
            _play.Points = points;
        }

        // public void ChoosePlaytype(PlayType playType)
        public ICollection<Play> GetPlays()
        {
            return _plays;
        }
    }
}
