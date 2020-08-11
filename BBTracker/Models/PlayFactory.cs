using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Models
{
    public class PlayFactory
    {
        private Play _play;
        private Play _resultingPlay;
        private Game _game;

        public PlayFactory(Game game)
        {
            _game = game;
        }
        

        public void CreateNewPlay(Player player, bool teamB)
        {
            if (_play is null)
            {
                _play = new Play
                {
                    Player = player,
                    TeamB = teamB,
                    Date = DateTime.Now.Date,
                    GameTime = DateTime.Now - _game.Start,
                    Game = _game
                };
            }
            else
            {
                _resultingPlay.Player = player;           _resultingPlay.TeamB = teamB; //do obadania
            }
        }

        public void ResultingPlay(PlayType playType, bool sameTeam)
        {
            _resultingPlay = new Play
            {
                PlayType = playType,
                Date = _play.Date,
                GameTime = _play.GameTime,
                Points = _play.Points != 0 ?
                    _play.Points : 0,
                TeamB = sameTeam ?
                    _play.TeamB : !_play.TeamB
            };
        }
       // public void ChoosePlaytype(PlayType playType)
        public ICollection<Play> GetPlays()
        {
            return _resultingPlay == null ?
                new List<Play> { _play } :
                new List<Play>{_play, _resultingPlay
                    };
        }
    }
}
