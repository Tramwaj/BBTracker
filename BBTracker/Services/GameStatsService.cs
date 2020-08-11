using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Services
{
    public class GameStatsService
    {
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public double TeamAFieldGoalPct { get; set; }
        public double TeamBFieldGoalPct { get; set; }
        public double TeamA3pFieldGoalPct { get; set; }
        public double TeamB3pFieldGoalPct { get; set; }
        public int TeamARebounds { get; set; }
        public int TeamBRebounds { get; set; }

        public int AFGA { get; set; }
        public int BFGA { get; set; }
        public int AFGM { get; set; }
        public int BFGM { get; set; }
        public int A3pFGA { get; set; }
        public int B3pFGA { get; set; }
        public int A3pFGM { get; set; }
        public int B3pFGM { get; set; }

        public GameStatsService(IEnumerable<Play> plays)
        {
            var _Aplays = plays.Where(p => p.TeamB == false);
            var _Bplays = plays.Where(p => p.TeamB == true);
            TeamAScore = Score(_Aplays);
            TeamBScore = Score(_Bplays);
            TeamAFieldGoalPct = FieldGoalPercentage(_Aplays);
            TeamBFieldGoalPct = FieldGoalPercentage(_Bplays);
            TeamA3pFieldGoalPct = ThreePointFieldGoalPrecentage(_Aplays);
            TeamB3pFieldGoalPct = ThreePointFieldGoalPrecentage(_Bplays);
            TeamARebounds = Rebounds(_Aplays);
            TeamBRebounds = Rebounds(_Bplays);
        }

        private double FieldGoalPercentage(IEnumerable<Play> plays)
        {
            var _fieldGoals = plays.Where(p => p.PlayType == PlayType.FieldGoal);
            var _fieldGoalsMade = _fieldGoals
                .Where(p => p.MadeFG == true).Count();
            if (_fieldGoalsMade == 0)
            {
                return 0;
            }
            return 100.0*
                _fieldGoalsMade
                / _fieldGoals.Count();
        }

        private double ThreePointFieldGoalPrecentage(IEnumerable<Play> plays)
        {
            return FieldGoalPercentage(plays.Where(p => p.Points == 3));
        }

        private int Score(IEnumerable<Play> plays)
        {
            return plays
                .Where(p => p.PlayType == PlayType.FieldGoal && p.MadeFG == true)
                .Sum(p => p.Points);
        }

        private int Rebounds(IEnumerable<Play> plays)
        {
            return plays
                .Where(p => p.PlayType == PlayType.Rebound)
                .Count();
        }
    }
}
