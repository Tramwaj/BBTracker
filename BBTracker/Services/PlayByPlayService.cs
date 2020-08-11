using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBTracker.Services
{
    public class PlayByPlayService
    {
        public Dictionary<DateTime,(string A,string B)> TimeLine { get; set; }
        
        public PlayByPlayService(IEnumerable<Play> plays)
        {
            StringBuilder sb = new StringBuilder();
        //    foreach (Play play in plays)
        //    {
        //        switch (play.PlayType)
        //        {
        //            case PlayType.FieldGoal:
        //                AddPreviousToTimeline(play.TeamB);
        //                sb.Clear();
        //                sb.Append(play.Player.FullName)
        //                    .Append(" - rzut za ")
        //                    .Append(play.Points)
        //                    .Append(" punkty");
        //        }
        //    }   
        }
        //private void AddPreviousToTimeline(bool TeamB)
        //{
        //    if (TimeLine.ContainsKey
        //}
    }
}
